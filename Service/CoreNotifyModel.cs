using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NewApp.Service
{
    [DebuggerStepThrough]
    [Serializable]
    public class CoreNotifyModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие для извещения об изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для вызова события извещения об изменении всех свойств
        /// </summary>
        /// <param name="propList"> Список свойств </param>
        public void OnAllPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// Метод для вызова события извещения об изменении свойства
        /// </summary>
        /// <param name="propertyName"> Изменившееся свойство </param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Метод для вызова события извещения об изменении списка свойств
        /// </summary>
        /// <param name="propList"> Список имён свойств </param>
        public void OnPropertyChanged(IEnumerable<string> propList)
        {
            foreach (var propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Метод для вызова события извещения об изменении перечня свойств
        /// </summary>
        /// <param name="propList"> Список имён свойств </param>
        public void OnPropertyChanged(params string[] propList)
        {
            foreach (var propertyName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Виртуальный метод изменяющий значение поля значения свойства
        /// </summary>
        /// <param name="fieldProperty"> Ссылка на поле значения свойства </param>
        /// <param name="newValue"> Новое значение </param>
        /// <param name="propertyName"> Название свойства </param>
        protected virtual void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            fieldProperty = newValue;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Виртуальный метод определяющий изменения в значении поля значения свойства
        /// </summary>
        /// <param name="fieldProperty"> Ссылка на поле значения свойства </param>
        /// <param name="newValue"> Новое значение </param>
        /// <param name="propertyName"> Название свойства </param>
        protected virtual void SetProperty<T>(ref T fieldProperty, T newValue,
            [CallerMemberName] string propertyName = "")
        {
            if (fieldProperty != null && !fieldProperty.Equals(newValue) || fieldProperty == null && newValue != null)
                PropertyNewValue(ref fieldProperty, newValue, propertyName);
        }
    }
}
