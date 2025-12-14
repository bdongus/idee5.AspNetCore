using idee5.Common.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace idee5.AspNetCore {
    /// <summary>
    /// Add validation results to the MVC model state.
    /// </summary>
    public class ModelStateReporter : IValidationResultReporter {
#pragma warning disable ASPDEPR006 // Typ oder Element ist veraltet
        // as long as M$ does not provide an alternative, we have to live with the obsolete warning
        /// <summary>
        /// The action context accessor.
        /// </summary>
        private readonly IActionContextAccessor _actionContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateReporter"/> class.
        /// </summary>
        /// <param name="actionContextAccessor">The action context accessor.</param>
        public ModelStateReporter(IActionContextAccessor actionContextAccessor) {
            _actionContextAccessor = actionContextAccessor;
        }
        /// <inheritdoc/>
        public void Report(ValidationResult validationResult) {
            _actionContextAccessor.ActionContext?.ModelState.AddModelError("", string.Format(validationResult.ErrorMessage ?? "", validationResult.MemberNames));
        }

        /// <inheritdoc/>
        public Task ReportAsync(ValidationResult validationResult, CancellationToken cancellationToken = default) {
            _actionContextAccessor.ActionContext?.ModelState.AddModelError("", string.Format(validationResult.ErrorMessage ?? "", validationResult.MemberNames));
            return Task.CompletedTask;
        }
#pragma warning restore ASPDEPR006 // Typ oder Element ist veraltet
    }
}
