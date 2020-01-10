using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.BLL.Infrastructure
{
    /// <summary>
    /// Класс возвращает в уровень представления ошибку, если она возникла в процессе валидации данных на уровне бизнес логики
    /// </summary>
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
