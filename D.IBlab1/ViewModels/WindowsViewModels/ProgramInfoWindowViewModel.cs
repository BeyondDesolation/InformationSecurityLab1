using D.IBlab1.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.IBlab1.ViewModels.WindowsViewModels
{
    internal class ProgramInfoWindowViewModel : ViewModelBase
    {
        #region type - Описание автора программы
        /// <summary>Описание автора программы </summary>
        private string _authorInfo;

        /// <summary>Описание автора программы </summary>
        public string AuthorInfo
        {
            get => _authorInfo;
            set => Set(ref _authorInfo, value);
        }
        #endregion


        #region type - Описание программы
        /// <summary>Описание программы </summary>
        private string _programDescription;

        /// <summary>Описание программы </summary>
        public string ProgramDescription
        {
            get => _programDescription;
            set => Set(ref _programDescription, value);
        }
        #endregion

        public ProgramInfoWindowViewModel()
        {
            // ЕЕЕ ХАРДКОООД
            AuthorInfo = "Автор: BeyondDesolation, GitHub (https://github.com/BeyondDesolation/InformationSecurityLab1)";

            ProgramDescription = "Лабораторная работа №1, вариант №6 \n" +
                "Разработка программы  разграничения полномочий пользователей на основе парольной" +
                " аутентификации c использованием встроенных криптопровайдеров. \n\n" +
                "Индивидуальные варианты заданий (ограничения на выбираемые  пароли):\n" +
                "Наличие букв и знаков арифметических операций.\n\n" +
                "Индивидуальные варианты заданий на использование криптографических методов\n" +
                "Используемый режим шифрования алгоритма DES: CBC\n" +
                "Добавление к ключу случайного значения: нет\n" +
                "Используемый алгоритм хеширования: SHA\n\n" +
                "Ульяновск, 2022";
        }
    }
}
