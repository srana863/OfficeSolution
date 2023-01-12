var Home = function () {
    var init = function () {
        alert(1);
        initialEvent();
        loadEmployeeProfiles();
    };
    var initialEvent = function () {
       
        $(document).on(click, '#searchTxt', function () {
            $('.contact-name').hide();
            var txt = $('#search-criteria').val();
            $('.contact-name:contains("' + txt + '")').show();
        });
        
    };
    
    var loadEmployeeProfiles = function () {
        alert(1);
        var url = "/Home/GetEmployeeProfiles";
        $("#formDetails").load(url);
    };
    return {
        init: init
    };
}();