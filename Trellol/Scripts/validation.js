var validate = {  

    add_error: function (field, error_msg, image_div) {
        field.addClass("field-validation-error").removeClass("field-validation-valid");
        image_div.remove();
        field.html(error_msg);
    },

    input: function (input_id, validation, div_id) {
        $(input_id).keyup(function () {
            var value = $(input_id).val();
            var field = $(div_id + " span.field-validation-valid");
            if (field.length == 0) field = $(div_id + " span.field-validation-error");
            var image_div = $(div_id + " img");
            if (validation.required && !value)
                validate.add_error(field, validation.required.error_msg, image_div);
            else if(validation.length && (validation.length.min > value.length || validation.length.max < value.length))
                validate.add_error(field, validation.length.error_msg, image_div);
            else if (validation.valid_chars && !(new RegExp(validation.valid_chars.regex).test(value)))
                validate.add_error(field, validation.valid_chars.error_msg, image_div);
            else if (validation.names && validation.names.existing_names.indexOf(value) >= 0)
                validate.add_error(field, validation.names.error_msg, image_div);
            else {
                field.addClass("field-validation-valid").removeClass("field-validation-error");
                if (image_div.length == 0) {
                    var checked = document.createElement("img");
                    checked.setAttribute('src', '/Content/images/icon-checked.gif');
                    $(div_id).append(checked);
                }
            }
        });


    }

};