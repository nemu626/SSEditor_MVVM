﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SSEditor
{
    public static class Util
    {
        /// <summary>
        /// From http://main.tinyjoker.net/Tech/CSharp/
        /// ディープコピー（深いコピー）をつくるCloneをジェネリック拡張メソッドでやってみた
        /// 
        /// ディープコピーを作成する。
        /// クローンするクラスには SerializableAttribute 属性、
        /// 不要なフィールドは NonSerializedAttribute 属性をつける。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T CloneDeep<T>(this T target)
        {
            object clone = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, target);
                stream.Position = 0;
                clone = formatter.Deserialize(stream);
            }
            return (T)clone;
        }
    }
}
