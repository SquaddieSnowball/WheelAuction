using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using WheelAuction.WebUI.Client.Resources.Validation.Services;

namespace WheelAuction.WebUI.Client.Validation.Services;

public class FluentValidationValidator : ComponentBase, IDisposable
{
	private static readonly ConcurrentDictionary<Type, IValidator> s_validatorCache = new();
	private static readonly char[] s_propertyPathSeparators = ['.', '['];

	private EditContext? _originalEditContext;
	private ValidationMessageStore? _validationMessages;

	[CascadingParameter]
	private EditContext? CurrentEditContext { get; set; }

	[Inject]
	public IServiceProvider ServiceProvider { get; set; } = default!;

	[Inject]
	public IStringLocalizer<FluentValidationValidator> StringLocalizer { get; set; } = default!;

	protected override void OnInitialized()
	{
		base.OnInitialized();

		if (CurrentEditContext is null)
		{
			throw new InvalidOperationException(StringLocalizer[
				FluentValidationValidatorResourceNames.ErrorMessageEditContextNull,
				typeof(FluentValidationValidator).FullName!,
				typeof(EditContext).FullName!]);
		}

		CurrentEditContext.OnValidationRequested += EditContextOnValidationRequested;
		CurrentEditContext.OnFieldChanged += EditContextOnFieldChanged;

		_originalEditContext = CurrentEditContext;
		_validationMessages = new ValidationMessageStore(CurrentEditContext);
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		if (CurrentEditContext != _originalEditContext)
		{
			throw new InvalidOperationException(StringLocalizer[
				FluentValidationValidatorResourceNames.ErrorMessageEditContextMissmatch,
				typeof(FluentValidationValidator).FullName!,
				typeof(EditContext).FullName!]);
		}
	}

	private void EditContextOnValidationRequested(object? sender, ValidationRequestedEventArgs e)
	{
		object model = CurrentEditContext!.Model;

		if (TryGetValidator(model, out IValidator? validator))
		{
			ValidationContext<object> validationContext = new(model);
			ValidationResult validationResult = validator.Validate(validationContext);

			_validationMessages!.Clear();

			foreach (ValidationFailure validationFailure in validationResult.Errors)
			{
				_validationMessages.Add(
					ConvertToFieldIdentifier(model, validationFailure.PropertyName),
					validationFailure.ErrorMessage);
			}

			CurrentEditContext.NotifyValidationStateChanged();
		}
	}

	private void EditContextOnFieldChanged(object? sender, FieldChangedEventArgs e)
	{
		object model = CurrentEditContext!.Model;
		FieldIdentifier fieldIdentifier = e.FieldIdentifier;

		if (TryGetValidator(model, out IValidator? validator))
		{
			ValidationContext<object> validationContext = new(
				model,
				new PropertyChain(),
				new MemberNameValidatorSelector([fieldIdentifier.FieldName]));

			ValidationResult validationResult = validator.Validate(validationContext);

			_validationMessages!.Clear(fieldIdentifier);

			if (validationResult.Errors.Count > 0)
			{
				_validationMessages.Add(
					fieldIdentifier,
					validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage));
			}

			CurrentEditContext.NotifyValidationStateChanged();
		}
	}

	private bool TryGetValidator(object model, [NotNullWhen(true)] out IValidator? validator)
	{
		Type modelType = model.GetType();

		if (s_validatorCache.TryGetValue(modelType, out validator))
			return true;

		Type validatorType = typeof(IValidator<>).MakeGenericType(modelType);
		validator = ServiceProvider.GetService(validatorType) as IValidator;

		if (validator is not null)
		{
			s_validatorCache.TryAdd(modelType, validator);

			return true;
		}

		return false;
	}

	private FieldIdentifier ConvertToFieldIdentifier(object model, string propertyName)
	{
		int tokenEndIndex = propertyName.IndexOfAny(s_propertyPathSeparators);

		if (tokenEndIndex is -1)
			return new FieldIdentifier(model, propertyName);

		ReadOnlySpan<char> propertyPath = propertyName;

		while (true)
		{
			ReadOnlySpan<char> token = propertyPath[..tokenEndIndex];
			propertyPath = propertyPath[(tokenEndIndex + 1)..];

			object? currentModel;

			if (!token.EndsWith("]"))
			{
				PropertyInfo? property = model.GetType().GetProperty(token.ToString())
					?? throw new InvalidOperationException(StringLocalizer[
						FluentValidationValidatorResourceNames.ErrorMessagePropertyNameConverterPropertyNotFound,
						token.ToString(),
						model.GetType().FullName!]);

				currentModel = property.GetValue(model);
			}
			else
			{
				token = token[..^1];

				PropertyInfo? property = model.GetType().GetProperty("Item");

				if (property is not null)
				{
					Type indexerType = property.GetIndexParameters()[0].ParameterType;
					object indexerValue = Convert.ChangeType(token.ToString(), indexerType);

					currentModel = property.GetValue(model, [indexerValue]);
				}
				else
				{
					switch (model)
					{
						case object[] array:
							{
								int indexerValue = int.Parse(token);

								currentModel = array[indexerValue];

								break;
							}

						case IReadOnlyList<object> readOnlyList:
							{
								int indexerValue = int.Parse(token);

								currentModel = readOnlyList[indexerValue];

								break;
							}

						default:
							throw new InvalidOperationException(StringLocalizer[
								FluentValidationValidatorResourceNames.ErrorMessagePropertyNameConverterIndexerNotFound,
								model.GetType().FullName!]);
					}
				}
			}

			if (currentModel is null)
				return new FieldIdentifier(model, token.ToString());

			tokenEndIndex = propertyPath.IndexOfAny(s_propertyPathSeparators);

			if (tokenEndIndex is -1)
				return new FieldIdentifier(currentModel, propertyPath.ToString());

			model = currentModel;
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			_validationMessages?.Clear();

			if (CurrentEditContext is not null)
			{
				CurrentEditContext.OnValidationRequested -= EditContextOnValidationRequested;
				CurrentEditContext.OnFieldChanged -= EditContextOnFieldChanged;

				CurrentEditContext.NotifyValidationStateChanged();
			}
		}
	}
}