using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CalculatorModel
    {
        [Required(ErrorMessage = "введите другое число")]
        public double? Operand1 { get; set; }
        [Range(1, 100, ErrorMessage = "число должно быть от 1 до 100, введите другое число")]
        public ulong Operand2 { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
    }
}