function ValidMail(email) {
    if (email == "") return true;
    var re = /^(?!.*@.*@.*$)(?!.*@.*\-\-.*\..*$)(?!.*@.*\-\..*$)(?!.*@.*\-$)(.*@.+(\..{1,11})?)$/;
    var valid = re.test(email);
    if (valid) output = 'Адрес эл. почты введен правильно!';
    else output = 'Адрес электронной почты введен неправильно!';
    //document.getElementById('message').innerHTML = output;
    console.log(output);
    ErrorText = output;
    return valid;
}
function ValidPhone(phone) {
    if (phone == "") return true;
    var re = /^([0-9]{0,11})?(\+[0-9]{1,3})?(\([0-9]{1,3})?(\)[0-9]{1})?([-0-9]{0,8})?([0-9]{0,1})?$/;
    var valid = re.test(phone);
    if (valid) output = 'Номер телефона введен правильно!';
    else output = 'Номер телефона введен неправильно!';
    //document.getElementById('message').innerHTML = document.getElementById('message').innerHTML + '<br />' + output;
    console.log(output);
    ErrorText = output;
    return valid;
}
var ErrorText = "";
function Validator(attr_name, attr_value) {
    if (attr_name == 'item.Email') {
        return ValidMail(attr_value);
    }

    if (attr_name == 'item.Phone') {
        return ValidPhone(attr_value);
    }

}



$(document).ready(function () {
    var j = jQuery.noConflict();
    j("#AddtableData").click(function (e) {
        $('#ContactInfo').append("<tr><td hidden=\"hidden\"><input data-val=\"true\" data-val-number=\"Значением поля Id должно быть число.\" data-val-required=\"Требуется поле Id.\" id=\"item_Id\" name=\"item.Id\" type=\"hidden\" value=\"0\"></td><td hidden=\"hidden\"><input data-val=\"true\" data-val-number=\"Значением поля ContactListId должно быть число.\" id=\"item_ContactListId\" name=\"item.ContactListId\" type=\"hidden\" value=\"\"></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line valid\" data-val=\"true\" data-val-length=\"Поле Телефон должно иметь строковое значение, максимальная длина которого — 50.\" data-val-length-max=\"50\" id=\"item_Phone\" name=\"item.Phone\" type=\"tel\" value=\"\" data-toggle=\"tooltip\" aria-describedby=\"item_Phone-error\" aria-invalid=\"false\"><span class=\"field-validation-valid text-danger\"></span></div></div></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line\" data-val=\"true\" data-val-length=\"Поле E-mail должно иметь строковое значение, максимальная длина которого — 50.\" data-val-length-max=\"50\" id=\"item_Email\" name=\"item.Email\" type=\"email\" value=\"\" data-toggle=\"tooltip\"><span class=\"field-validation-valid text-danger\"></span></div></div></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line\" data-val=\"true\" data-val-length=\"Поле Skype должно иметь строковое значение, максимальная длина которого — 50.\" data-val-length-max=\"50\" id=\"item_Skype\" name=\"item.Skype\" type=\"text\" value=\"\"><span class=\"field-validation-valid text-danger\" data-valmsg-for=\"item.Skype\" data-valmsg-replace=\"true\"></span></div></div></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line\" data-val=\"true\" data-val-length=\"Поле Другое должно иметь строковое значение, максимальная длина которого — 500.\" data-val-length-max=\"500\" id=\"item_Other\" name=\"item.Other\" type=\"text\" value=\"\"><span class=\"field-validation-valid text-danger\" data-valmsg-for=\"item.Other\" data-valmsg-replace=\"true\"></span></div></div></td><td><a href=\"#\" class=\"row-remove\">Удалить</a></td></tr>");
        $('input').on('input', function (e) {
            $("input[name='JsonFile']").val(html2json());
        });
        ValidatorAndInner();
    });
    
    ValidatorAndInner();

    $('input[type=submit]').attr('data-toggle', 'tooltip');

    $('input[type=submit]').submit(function () {
        var boolean = true;
        var tbl2 = $('#ContactInfo tbody tr').each(function (i) {
            x = $(this).children();
            var itArr = [];
            x.each(function () {
                //itArr.push('"' + $(this).find('input').attr("name") + '":"' + $(this).find('input').val() + '"');
                let bool = Validator($(this).find('input').attr("name"), $(this).find('input').val());
                if (bool == false)
                    boolean = false;
            });
        });
        return boolean;
    });
});

function html2json() {
    var json = '[';
    var otArr = [];
    var tbl2 = $('#ContactInfo tbody tr').each(function (i) {
        x = $(this).children();
        var itArr = [];
        x.each(function () {
            itArr.push('"' + $(this).find('input').attr("name") + '":"' + $(this).find('input').val() + '"');
        });
        otArr.push('{' + itArr.join(',') + '}');
    })
    json += otArr.join(",") + ']'

    return json;

}

function ValidatorAndInner() {
    $("input[name='JsonFile']").val(html2json());
    $('input').on('input', function () {
        $("input[name='JsonFile']").val(html2json());
        let valid = Validator($(this).attr('name'), $(this).val());
        $(this).attr('data-toggle', 'tooltip');
        if (valid == false) {
            $('input[type=submit]').prop('disabled', true);
            $(this).attr('title', ErrorText);
            $(this).next().text(ErrorText);
            $('input[type=submit]').attr('title', ErrorText);
        }
        else {
            $('input[type=submit]').prop('disabled', false);
            $(this).removeAttr('title');
            $(this).next().text("");
            $('input[type=submit]').removeAttr('title');
        }
    });

    $('.row-remove').click(function (e) {
        e.preventDefault();
        $(this).closest('tr').remove(); // или $(this).parent().parent().remove();
        $("input[name='JsonFile']").val(html2json());
    });
}

$('[data-toggle="tooltip"]').tooltip();