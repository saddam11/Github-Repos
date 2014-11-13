using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGenerator.Helper
{
    interface IUserGenerator
    {
        string[] RandomName(int count, int nameLength);
        string[] RandomNumber(int count, DateParts dateParts);
    }
}
