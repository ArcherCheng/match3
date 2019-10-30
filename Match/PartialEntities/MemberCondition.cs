using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Entities
{
    public partial class MemberCondition : EntityBase, IEntityBase, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.Sex == 0)
            {
                yield return new ValidationResult("性別不能空白", new string[] { "Sex" });
            }
        }
    }
}
