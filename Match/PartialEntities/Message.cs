using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Match.Entities
{
    public partial class Message : EntityBase, IEntityBase, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.SenderId == this.RecipientId)
            {
                yield return new ValidationResult("不能自已給自己訊息", new string[] { "RecipientId" });
            }
        }
    }
}
