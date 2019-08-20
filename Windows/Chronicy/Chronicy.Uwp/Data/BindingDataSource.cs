﻿using Chronicy.Data;
using Chronicy.Data.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Chronicy.Uwp.Data
{
    public class BindingDataSource<T> : IBindingList where T : IStoredObject
    {
        public IDataSource<T> DataSource { get; set; }

        public IList<T> Items => DataSource.GetAll().ToList();

        public object this[int index] { get => Items[index]; set => Items[index] = (T)value; }

        public bool AllowEdit => true;

        public bool AllowNew => true;

        public bool AllowRemove => true;

        public bool IsSorted => throw new NotImplementedException();

        public ListSortDirection SortDirection => throw new NotImplementedException();

        public PropertyDescriptor SortProperty => throw new NotImplementedException();

        public bool SupportsChangeNotification => throw new NotImplementedException();

        public bool SupportsSearching => throw new NotImplementedException();

        public bool SupportsSorting => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public int Count => Items.Count;

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public event ListChangedEventHandler ListChanged;

        public int Add(object value)
        {
            if (!(value is T item))
            {
                throw new ArgumentException("The object is not of the required type");
            }

            DataSource.Create(item);

            return Items.Count - 1;
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public object AddNew()
        {
            throw new NotImplementedException();
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (T item in DataSource.GetAll())
            {
                DataSource.Delete(item.ID);
            }
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return DataSource.GetAll().GetEnumerator();
        }

        public int IndexOf(object value)
        {
            if (!(value is T item))
            {
                throw new ArgumentException("The object is not of the required type");
            }

            return Items.IndexOf(item);
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            if (!(value is T item))
            {
                throw new ArgumentException("The object is not of the required type");
            }

            DataSource.Delete(item.ID);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void RemoveSort()
        {
            throw new NotImplementedException();
        }
    }
}
