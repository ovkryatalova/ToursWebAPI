//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToursWebAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TypeOfTour
    {
        public int TypeId { get; set; }
        public int TourId { get; set; }
    
        public virtual Tour Tour { get; set; }
        public virtual Type Type { get; set; }
    }
}
