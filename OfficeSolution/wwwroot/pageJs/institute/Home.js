var Home = function () {
    var init = function () {
        alert(1);
        initialEvent();
        loadFacultyProfiles();
    };
    var initialEvent = function () {
       
        $(document).on(click, '#searchTxt', function () {
            $('.contact-name').hide();
            var txt = $('#search-criteria').val();
            $('.contact-name:contains("' + txt + '")').show();
        });
        
    };
    
    var loadFacultyProfiles = function () {
        alert(1);
        var url = "/Home/GetFacultyProfiles";
        $("#formDetails").load(url);
    };
    return {
        init: init
    };
}();