using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cinode.Skills.Api.Repositories.DataModels
{
    [DataContract]
    public class Skill
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Guid ExternalId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int RatingPercentage { get; set; }

        [DataMember]
        public DateTime? Created { get; internal set; }
    }
}
