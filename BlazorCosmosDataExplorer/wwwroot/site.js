'use strict';

// Excel Export
function downloadFileFromBase64(filename, data) {
    const link = document.createElement('a');
    link.href = data;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

// Prevent Shift+Enter from inserting a newline
window.preventShiftEnter = function (elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.addEventListener('keydown', function(event) {
            if (event.key === 'Enter' && event.shiftKey) {
                event.preventDefault(); 
            }
        });
    }
};

// Loading bar
$(document).ready(function() {
    $('#submit_button').click(function() {
        $('.loading_bar_container').removeClass('hide_me');
    });
});
