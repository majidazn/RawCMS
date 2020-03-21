﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina'</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
namespace RawCMS.Library.Schema.FieldTypes
{
    public class ListType : FieldType
    {
        public override string TypeName => "list";

        public override FieldGraphType GraphType => FieldGraphType.String;

        public ListType()
        {
            //
            OptionParameter.Add(new OptionParameter()
            {
                Name = "allowNotMapped",
                Type = "bool",
                Description = "true if you allow values not listed into values"
            });

            //comma separated values
            OptionParameter.Add(new OptionParameter()
            {
                Name = "values",
                Type = "text",
                Description = "comma separated values. Format is  A|B|C or 1=A|2=B. | and = are not allowed within values"
            });
        }
    }
}