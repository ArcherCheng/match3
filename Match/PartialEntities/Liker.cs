using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Match.Entities
{
    public partial class Liker : EntityBase, IEntityBase, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.UserId == this.LikerId)
            {
                yield return new ValidationResult("不能自已加自己好友", new string[] { "LikerId" });
            }
        }
    }
}
