﻿/***************************************************************************
 * HebMorph - making Hebrew properly searchable
 * 
 *   Copyright (C) 2010-2012                                               
 *      Itamar Syn-Hershko <itamar at code972 dot com>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

namespace HebMorph.DataStructures
{
    using System;
    using System.Collections.Generic;

    public enum SortOrder { Asc, Desc };

    public class RealSortedList<T> : List<T>
    {
        protected SortOrder sortOrder = SortOrder.Asc;

        #region Constructors
        public RealSortedList()
            : base()
        {
        }

        public RealSortedList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public RealSortedList(int capacity)
            : base(capacity)
        {
        }

        public RealSortedList(SortOrder _sortOrder)
            : base()
        {
            this.sortOrder = _sortOrder;
        }

        public RealSortedList(IEnumerable<T> collection, SortOrder _sortOrder)
            : base(collection)
        {
            this.sortOrder = _sortOrder;
        }

        public RealSortedList(int capacity, SortOrder _sortOrder)
            : base(capacity)
        {
            this.sortOrder = _sortOrder;
        }
        #endregion

        /// <summary>
        /// Add item only if it doesn't exist in the collection already. T has to have Equals implemented.
        /// </summary>
        /// <param name="item">Item of type T to add</param>
        /// <returns>true if added, false otherwise</returns>
        public bool AddUnique(T item)
        {
            List<T>.Enumerator en = GetEnumerator();
            while (en.MoveNext())
            {
                if (en.Current.Equals(item))
                    return false;
            }
            this.Add(item);
            return true;
        }

        public new void Add(T item)
        {
            if (Count == 0)
            {
                base.Add(item);
                return;
            }

            int i = 0, cmp = 0;
            Comparer<T> comparer = Comparer<T>.Default;
            List<T>.Enumerator en = GetEnumerator();
            while (en.MoveNext())
            {
                cmp = comparer.Compare(en.Current, item);
                if ((sortOrder == SortOrder.Desc && cmp < 0) || (sortOrder == SortOrder.Asc && cmp > 0))
                    break;

                i++;
            }
            base.Insert(i, item);
        }
    }
}
