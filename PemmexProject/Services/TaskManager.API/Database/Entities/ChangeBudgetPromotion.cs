using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class ChangeBudgetPromotion
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int BudgetTaskId { get; set; }
        public List<ChangeBudgetPromotionDetail> changeBudgetPromotionDetails { get; set; }
        public virtual BaseTask BaseTask { get; set; }
    }

}
