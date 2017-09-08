﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OneLine {
    internal class FixedArrayDrawer : ComplexFieldDrawer {

        public FixedArrayDrawer(DrawerProvider getDrawer) : base(getDrawer) {
        }

        protected override IEnumerable<SerializedProperty> GetChildren(SerializedProperty property){
            return property.GetArrayElements();
        }

        public override void Draw(Rect rect, SerializedProperty property) {
            GetLength(property);
            base.Draw(rect, property);
        }

        protected override void DrawField(Rect rect, SerializedProperty element) {
            getDrawer(element).Draw(rect, element);
        }

        protected virtual int GetLength(SerializedProperty property) {
            var attribute = property.GetCustomAttribute<ArrayLengthAttribute>();
            if (attribute == null) {
                var message = string.Format("Can not find ArrayLengthAttribute at property {1)", property.propertyPath);
                throw new InvalidOperationException(message);
            }
            property.arraySize = attribute.Length;
            return property.arraySize;
        }

    }
}
