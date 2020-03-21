﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina'</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
namespace RawCMS.Library.Schema.Validation
{
    public class TextValidation : BaseJavascriptValidator
    {
        public override string Type => "text";

        public override string Javascript
        {
            get
            {
                return @"
const innerValidation = function() {
    if (value === null || value === undefined) {
        return;
    }

    // code starts here
    if (typeof(value) !== 'string') {
        errors.push({""Code"":""STRING-01"", ""Title"":""Not a string""});
        return;
    }

    if (options.maxlength !== undefined && options.maxlength < value.length) {
        errors.push({""Code"":""STRING-02"", ""Title"":""field too long"",""Description"":""ddd""});
    }

    if (options.regexp !== undefined)
    {
        var re = new RegExp(options.regexp);

        if (!value.match(re))
        {
           errors.push({""Code"":""STRING-02"", ""Title"":""field do not match the regular expression ""});
        }
    }

    return JSON.stringify(errors);
};

var backendResult = innerValidation();
            ";
            }
        }
    }
}