// 格式化时间    
function FormatJsonTime(date) {
    if (date != null) {
        var date = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
        var y = date.getFullYear();
        var m = (date.getMonth() + 1) < 10 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
        var d = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var h = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        var M = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
        var s = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
        //console.log(value, h)
        return y + '-' + m + '-' + d + ' ' + h + ':' + M + ':' + s;


    }
    else {
        return "";
    }
};

// 格式化日期
function FormatJsonDate(date) {
    if (date != null) {
        var date = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
        var y = date.getFullYear();
        var m = (date.getMonth()) + 1 < 10 ? "0" + date.getMonth()+1 : (date.getMonth()+1);
        var d = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

        return y + '-' + m + '-' + d
    }
    else {
        return "";
    }

}
