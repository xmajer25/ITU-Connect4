using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class RouteModel
    {
        public string Route { get; }
        public Type ViewType { get; }
        public Type ViewModelType { get; }

        public RouteModel(string route, Type viewType, Type viewModelType)
        {
            Route = route;
            ViewType = viewType;
            ViewModelType = viewModelType;
        }
    }
}
