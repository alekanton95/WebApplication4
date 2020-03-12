$(document).ready(function () {

});


function ValidMail(email) {
    if (email == "") return true;
    var re = /^[\w-\.]+@[\w-]+\.[a-z]{2,4}$/i;
    var valid = re.test(email);
    if (valid) output = 'Адрес эл. почты введен правильно!';
    else output = 'Адрес электронной почты введен неправильно!';
    //document.getElementById('message').innerHTML = output;
    console.log(output);
    return valid;
}

function ValidPhone(phone) {
    if (phone == "") return true;
    var re = /^\d[\d\(\)\ -]{4,14}\d$/;
    var valid = re.test(phone);
    if (valid) output = 'Номер телефона введен правильно!';
    else output = 'Номер телефона введен неправильно!';
    //document.getElementById('message').innerHTML = document.getElementById('message').innerHTML + '<br />' + output;
    console.log(output);
    return valid;
}  

function Validator(attr_name,attr_value) {
    if (attr_name == 'item.Email')
        return ValidMail(attr_value);
    if (attr_name == 'item.Phone')
        return ValidPhone(attr_value);
}



$(document).ready(function () {
    var j = jQuery.noConflict();
    j("#AddtableData").click(function (e) {
        $('#ContactInfo').append("<tr><td hidden=\"hidden\"><input id=\"item_Id\" name=\"item.Id\" type=\"hidden\" value=\"-1\"></td><td hidden=\"hidden\"><input id=\"item_ContactListId\" name=\"item.ContactListId\" type=\"hidden\" value=\"\"></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line\" id=\"item_Phone\" name=\"item.Phone\" type=\"tel\" value=\"\"><span class=\"field-validation-valid text-danger\" data-valmsg-for=\"item.Phone\" data-valmsg-replace=\"true\"></span></div></div></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line\" id=\"item_Email\" name=\"item.Email\" type=\"email\" value=\"\"><span class=\"field-validation-valid text-danger\" data-valmsg-for=\"item.Email\" data-valmsg-replace=\"true\"></span></div></div></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line\" id=\"item_Skype\" name=\"item.Skype\" type=\"text\" value=\"\"><span class=\"field-validation-valid text-danger\" data-valmsg-for=\"item.Skype\" data-valmsg-replace=\"true\"></span></div></div></td><td><div class=\"form-group\"><div class=\"col-md-10\"><input class=\"form-control text-box single-line\" id=\"item_Other\" name=\"item.Other\" type=\"text\" value=\"\"><span class=\"field-validation-valid text-danger\" data-valmsg-for=\"item.Other\" data-valmsg-replace=\"true\"></span></div></div></td></tr>");
        $('input').on('input', function (e) {
            $("input[name='JsonFile']").val(html2json());
        });

    });
    $("input[name='JsonFile']").val(html2json());
    $('input').on('input', function () {
        $("input[name='JsonFile']").val(html2json());
        Validator($(this).attr('name'),$(this).val());
    });

    $('.row-remove').click(function (e) {
        e.preventDefault();
        $(this).closest('tr').remove(); // или $(this).parent().parent().remove();
        $("input[name='JsonFile']").val(html2json());
    });

    $('input[type=submit]').attr('disabled', 'true');

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