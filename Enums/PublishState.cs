using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockwellBlog.Enums
{
    public enum PublishState
    {
        
        ProductionReady,
        PreviewReady,
        NotReady
    }
}