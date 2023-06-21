using idee5.Common.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace idee5.AspNetCore {
    /// <summary>
    /// Add validation results to the MVC model state.
    /// </summary>
    public class ModelStateReporter : IValidationResultReporter {
        private readonly IActionContextAccessor actionContextAccessor;

        public ModelStateReporter(IActionContextAccessor actionContextAccessor) {
            this.actionContextAccessor = actionContextAccessor;
        }
        /// <inheritdoc/>
        public void Report(ValidationResult validationResult) {
            actionContextAccessor.ActionContext?.ModelState.AddModelError("", string.Format(validationResult.ErrorMessage ?? "", validationResult.MemberNames));
        }

        /// <inheritdoc/>
        public Task ReportAsync(ValidationResult validationResult, CancellationToken cancellationToken = default) {
            actionContextAccessor.ActionContext?.ModelState.AddModelError("", string.Format(validationResult.ErrorMessage ?? "", validationResult.MemberNames));
            return Task.CompletedTask;
        }
    }
}
