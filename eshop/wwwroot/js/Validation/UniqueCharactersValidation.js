$.validator.addMethod('uniquecharacters', function (value, element, params) {

    var charactersCountRequired = 6;
    var password = value;
    var counts = {};

    var counts = {};

    // Misc vars
    var ch, index, len, count;

    // Loop through the string...
    for (index = 0, len = password.length; index < len; ++index) {
        // Get this character
        ch = password.charAt(index); // Not all engines support [] on strings

        count = counts[ch];

        counts[ch] = count ? count + 1 : 1;
    }

    if (Object.keys(counts).length >= charactersCountRequired) {
        return true;
    }
    return false;
});

$.validator.unobtrusive.adapters.add('uniquecharacters', ['charactersCountRequired'],
    function (options) {

        var element = $(options.form).find('#password')[0];

        options.rules['uniquecharacters'] = [element, parseInt(options.params['charactersCountRequired'])];
        options.messages['uniquecharacters'] = options.message;
    });