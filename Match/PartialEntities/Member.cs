using Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Entities
{
    public partial class Member : EntityBase, IEntityBase, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.BirthYear < System.DateTime.Now.Year - 72)
            {
                yield return new ValidationResult("年齡必須在72歲以內", new string[] { "BirthYear" });
            }

            if (this.BirthYear > System.DateTime.Now.Year - 12)
            {
                yield return new ValidationResult("年齡必須在12歲以上", new string[] { "BirthYear" });
            }

            if (this.Salary > 10000)
            {
                yield return new ValidationResult("年薪最大為1億", new string[] { "Salary" });
            }

            if (this.Heights > 200)
            {
                yield return new ValidationResult("身高最大為200公分", new string[] { "Heights" });
            }

            if (this.Weights > 120)
            {
                yield return new ValidationResult("體重最重為120公斤", new string[] { "Weights" });
            }
        }
    }
}
