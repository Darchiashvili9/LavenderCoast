//const { css } = require("jquery");


var currentdate = new Date();
var datetime = currentdate.getMonth() + 1 + "/" + (currentdate.getDate()) + "/" + currentdate.getFullYear();
console.log(datetime);

$(document).ready(function () {
    $('#datepicker').val("Выберите Дату");
    $('#datepicker2').val("Выберите Дату");

    $("#outP1").hide();
    $("#outP2").hide();
    $("#outP3").hide();
    $("#outP4").hide();

    $("#orderCurrencyVal").hide();
})



$("#siteRulesPopup").on("click", function () {
    let baseUri = "https://localhost:44338";

    $.ajax({
        url: baseUri + "/Home/siteRules",
        type: "GET",
        ContentType: "application/json",

        success: function (data, textStatus, jqXHR) {
            console.log("1111111111111");
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in siterules');
        }
    });

})

$(".product__btn").on("click", function () {
    quantityChangeFunc();
})

/*JqueryUI*/
$('#buttonVisit1').on("click", function () {


    $('#OrderTiming').empty();
    $('#ProductQuantity').val("");
    $('#datepicker').val("Выберите Дату");
    $('#datepicker2').val("");

    $("#outP1").hide();
    $("#outP2").hide();
    $("#outP3").hide();
    $("#outP4").hide();
    $("#orderCurrencyVal").hide();
    $('#datepicker2').hide();
    $('#datepicker').show();

    var newOption = $('<option value="0">Выберите Время</option>');
    $('#OrderTiming').append(newOption);
    document.getElementById("orderPriceVal").innerHTML = "";

    //   debugger;
    console.log("buttonVisit1");
    let baseUri = "https://localhost:44338";
    $.ajax({
        url: baseUri + "/Products/VisitBookedDatesGet",
        type: "GET",
        ContentType: "application/json",

        success: function (data, textStatus, jqXHR) {
            //ეს მაგალითია ფორმატირების უბრალოდ;
            //$.format.date('2011-08-31T20:01:32.000Z', "dd-MM-yyyy"));
            //$.format.date('2011-08-31T20:01:32.000Z', "hh:mm"));

            //კალენდარს საბმითზე შეგიძლია კიდევ ერთი ვალიდაცია ჩაურთო რო კიდე ერთხელ გაატაროს ვინმემ მაგალითად რო თუ ხელით შეიყვანა თარიღი ეგ შეუმოწმოს
            //ანუ თუკი იუზერმა არა კალენდრიდან არამედ ხელით შეიყვანა სისტემის მოტყუების მცდელობით, მაშინ გადაამოწმოს კიდე ერთხელ კალენდრის ველიუ;

            let arr = new Array();
            if (data != null)
                for (var i = 0; i < data.length; i++) {
                    arr[i] = data[i].split(' ')[0];
                    console.log(arr[i]);
                }

            $('#datepicker').datepicker({
                minDate: 0,     //-20,        // минимальная дата
                maxDate: "10D",       //"+3M +10D",  // максимальная дата (+ 1 месяц и 10 дней)
                beforeShowDay: function (date) {

                    //m/dd/yy ეს ფორმატია მნიშვნელოვანი 0 იანი ანუ 02 თვე რო გამოჩნდეს მაშინ mm უნდა იყოს წინ და ა.შ
                    //აქ ფორმატები ხშირად ირევა სერვერზე ('dd.mm.yy', date); ეს ფორმატი ეშვება მარა ლოკალურად ('m/d/yy', date); ეს
                    var string = jQuery.datepicker.formatDate('dd.mm.yy', date);
                    return [arr.indexOf(string) == -1, 'red']
                },
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');
        }
    });
});

$('#buttonMasterClass').on("click", function () {

    $('#OrderTiming').empty();
    $('#ProductQuantity').val("");
    $('#datepicker2').val("Выберите Дату");
    $('#datepicker').val("");

    $("#outP1").hide();
    $("#outP2").hide();
    $("#outP3").hide();
    $("#outP4").hide();
    $("#orderCurrencyVal").hide();
    $('#datepicker').hide();
    $('#datepicker2').show();

    var newOption = $('<option value="0">Выберите Время</option>');
    $('#OrderTiming').append(newOption);
    document.getElementById("orderPriceVal").innerHTML = "";


    // debugger;
    console.log("buttonMasterClass");
    let baseUri = "https://localhost:44338";
    $.ajax({
        url: baseUri + "/Products/MasterClassBookedDatesGet",
        type: "GET",
        ContentType: "application/json",

        success: function (data, textStatus, jqXHR) {
            //ეს მაგალითია ფორმატირების უბრალოდ;
            //$.format.date('2011-08-31T20:01:32.000Z', "dd-MM-yyyy"));
            //$.format.date('2011-08-31T20:01:32.000Z', "hh:mm"));
            //კალენდარს საბმითზე შეგიძლია კიდევ ერთი ვალიდაცია ჩაურთო რო კიდე ერთხელ გაატაროს ვინმემ მაგალითად რო თუ ხელით შეიყვანა თარიღი ეგ შეუმოწმოს
            //ანუ თუკი იუზერმა არა კალენდრიდან არამედ ხელით შეიყვანა სისტემის მოტყუების მცდელობით, მაშინ გადაამოწმოს კიდე ერთხელ კალენდრის ველიუ;

            let arr = new Array();
            if (data != null)
                for (var i = 0; i < data.length; i++) {
                    arr[i] = data[i].split(' ')[0];
                    console.log(arr[i]);
                }

            $('#datepicker2').datepicker({
                minDate: 0,     //-20,        // минимальная дата
                maxDate: "10D",       //"+3M +10D",  // максимальная дата (+ 1 месяц и 10 дней)
                beforeShowDay: function (date) {
                    //m/dd/yy ეს ფორმატია მნიშვნელოვანი 0 იანი ანუ 02 თვე რო გამოჩნდეს მაშინ mm უნდა იყოს წინ და ა.შ

                    //აქ ფორმატები ხშირად ირევა სერვერზე ('dd.mm.yy', date); ეს ფორმატი ეშვება მარა ლოკალურად ('m/d/yy', date); ეს
                    var string = jQuery.datepicker.formatDate('dd.mm.yy', date);
                    return [arr.indexOf(string) == -1, 'red']
                },
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Database');
        }
    });
});



/*DropDown*/
$('.datePickerMyClass').on("change", function () {
    //  debugger

    let baseUri = "https://localhost:44338";




    //აქ იქნება ერთი ფორით შესამოწმებელი რომ კენდოს კალენდრის ველის ველიუ მიმდინარედ 
    //უნდა შედარდეს ფორით მთელს მასივთან იფ პირობით რომ თუ ტოლია მაშინ ყოველ ტოლობაზე უნდა გამოაკლდეს სიიდან ან მორნინგთაიმი ან დეითაიმი;
    //ყველაზე მთავარი --> შედარება უნდა მოხდეს ანუ კალენდრის ველიუს წამოღება უნდა მოხდეს დროპდაუნზე კლიკის მომენტში
    //ე.ი დაკლიკა დროფდაუნზე და ჯქვერიმ წამოიღო კალენდრის ველიუ, ფორში გაატარა და დაფილტრა დროფდაუნის ველები;
    //გამოდის რო ეს ამ მომენტში საჭირო არცაა ეს ფუნქცია აქ არ უნდა ეწეროს, ეს გამოდის დროფდაუნზე დაკლიკვის მომენტში მხოლოდ;

    if (document.getElementById("orderNameVal").innerHTML == "Посещение") {
        let dateVal = $('#datepicker').val();

        $.ajax({
            url: baseUri + "/Products/GetTakenTimesByDate/",
            type: "POST",
            data: JSON.stringify(dateVal),
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (data, textStatus, jqXHR) {
                //debugger


                //data ში წამოვა მთლიანად პროდუქტები დროის მიხედვით დაჯგუფებული, 
                //სათითაოდ უნდა შედარდეს ეს პროდუქტები რაოდენობებს - 15,200,15 დროის მიხედვით და გაიცხრილოს,
                //თან აქვე უნდა გაკეთდეს თეგი რომელსაც მივცემთ დარჩენილი ადგილების რაოდენობას; - ამისთვის მაინც ახალი პროცედურის გაკეთებაა საჭირო.


                $('#OrderTiming').empty(); //remove all child nodes

                var newOption = $('<option value="0">Выберите Время</option>');
                var newOption1 = $('<option value="1">с 5:00 до 8:00</option>');
                var newOption2 = $('<option value="2">с 8:00 до 16:00</option>');
                var newOption3 = $('<option value="3">с 18:00 до 22:00</option>');

                if (data.length < 1)
                    $('#OrderTiming').append(newOption);

                $("#outP1").val("Оставшиеся билеты: " + 15);
                $("#outP2").val("Оставшиеся билеты: " + 200);
                $("#outP3").val("Оставшиеся билеты: " + 15);

                $("#outP1").hide();
                $("#outP2").hide();
                $("#outP3").hide();
                $("#outP4").hide();


                if (data == undefined || data.length < 1) {
                    $('#OrderTiming').append(newOption);
                    $('#OrderTiming').append(newOption1);
                    $('#OrderTiming').append(newOption2);
                    $('#OrderTiming').append(newOption3);
                }

                else {
                    let countAvaliebleTickets1;
                    let countAvaliebleTickets2;
                    let countAvaliebleTickets3;


                    $('#OrderTiming').append(newOption);

                    for (var i = 0; i < data.length; i++) {

                        if (data[i].orderReceiveTime == 1) {
                            if (data[i].productQuantity < 15) {
                                $('#OrderTiming').append(newOption1);
                                countAvaliebleTickets1 = 15 - data[i].productQuantity;
                                $("#outP1").val("Оставшиеся билеты: " + (15 - data[i].productQuantity));

                            }
                            if (i + 1 == data.length) {
                                $('#OrderTiming').append(newOption2);
                                countAvaliebleTickets2 = 200;
                                $("#outP2").val("Оставшиеся билеты: " + 200);

                                $('#OrderTiming').append(newOption3);
                                countAvaliebleTickets3 = 15;
                                $("#outP3").val("Оставшиеся билеты: " + 15);
                            }
                        }

                        if (data[i].orderReceiveTime == 2) {
                            if (data[i].productQuantity < 200) {
                                $('#OrderTiming').append(newOption2);
                                countAvaliebleTickets2 = 200 - data[i].productQuantity;
                                $("#outP2").val("Оставшиеся билеты: " + (200 - data[i].productQuantity));
                            }
                            if (i + 1 == data.length && data.length != 1) {
                                $('#OrderTiming').append(newOption3);
                                countAvaliebleTickets3 = 15;
                                $("#outP3").val("Оставшиеся билеты: " + 15);
                            }
                            if (i + 1 == data.length && data.length == 1) {
                                $('#OrderTiming').append(newOption1);
                                countAvaliebleTickets1 = 15;
                                $("#outP1").val("Оставшиеся билеты: " + 15);

                                $('#OrderTiming').append(newOption3);
                                countAvaliebleTickets3 = 15;
                                $("#outP3").val("Оставшиеся билеты: " + 15);
                            }
                        }

                        if (data[i].orderReceiveTime == 3) {
                            if (data[i].productQuantity < 15) {
                                $('#OrderTiming').append(newOption3);
                                countAvaliebleTickets3 = 15 - data[i].productQuantity;
                                $("#outP3").val("Оставшиеся билеты: " + (15 - data[i].productQuantity));
                            }

                            if (i + 1 == data.length && data.length == 1) {
                                $('#OrderTiming').append(newOption1);
                                countAvaliebleTickets1 = 15;
                                $("#outP1").val("Оставшиеся билеты: " + 15);

                                $('#OrderTiming').append(newOption2);
                                countAvaliebleTickets2 = 200;
                                ("#outP2").val("Оставшиеся билеты: " + 200);
                            }

                            if (i + 1 == data.length && data.length == 2 && data[i - 1].orderReceiveTime == 1) {
                                $('#OrderTiming').append(newOption2);
                                countAvaliebleTickets2 = 200;
                                $("#outP2").val("Оставшиеся билеты: " + 200);
                            }

                            if (i + 1 == data.length && data.length == 2 && data[i - 1].orderReceiveTime == 2) {
                                $('#OrderTiming').append(newOption1);
                                countAvaliebleTickets1 = 15;
                                $("#outP1").val("Оставшиеся билеты: " + 15);
                            }

                        }
                    }
                    console.log(countAvaliebleTickets1)
                    console.log(countAvaliebleTickets2)
                    console.log(countAvaliebleTickets3)

                }
                $('#OrderTiming').trigger("chosen:updated");
            }
        });
    }
    else if (document.getElementById("orderNameVal").innerHTML == "Мастеркласс") {
        let dateVal = $('#datepicker2').val();

        $.ajax({
            url: baseUri + "/Products/getTakenTimesByDateMasterClass/",
            type: "POST",
            data: JSON.stringify(dateVal),
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (data, textStatus, jqXHR) {
                //   debugger

                $('#OrderTiming').empty(); //remove all child nodes
                //    var newOption = $('<option value="0">Выберите Время</option>');
                var newOption4 = $('<option value="4">с 16:00 до 18:00</option>');

                $("#outP4").val("Оставшиеся билеты: " + 15);
                $("#outP1").hide();
                $("#outP2").hide();
                $("#outP3").hide();
                $("#outP4").show();

                //      $('#OrderTiming').append(newOption);
                $('#OrderTiming').append(newOption4);
                countAvaliebleTickets1 = 15 - data[0].productQuantity;
                if (data[0].productQuantity != undefined && data[0].productQuantity != null) {
                    $("#outP4").val("Оставшиеся билеты: " + (15 - data[0].productQuantity));
                }
                $('#OrderTiming').trigger("chosen:updated");

                OrderTimingChange();
            }
        });
    }
});

$('#OrderTiming').on("change", function () {
    OrderTimingChange();
});

function OrderTimingChange() {
    //  debugger;
    let myOrderTime = $('#OrderTiming').val();

    if (myOrderTime == 0) {
        $("#outP1").hide();
        $("#outP2").hide();
        $("#outP3").hide();
    }

    if (myOrderTime == 1) {
        $("#outP1").show();
        $("#outP2").hide();
        $("#outP3").hide();
        document.getElementById("orderPriceVal").innerHTML = "500 ₽";
        // $("#orderCurrencyVal").show();
    }

    if (myOrderTime == 2) {
        $("#outP1").hide();
        $("#outP2").show();
        $("#outP3").hide();
        document.getElementById("orderPriceVal").innerHTML = "300 ₽";
        //    $("#orderCurrencyVal").show();
    }

    if (myOrderTime == 3) {
        $("#outP1").hide();
        $("#outP2").hide();
        $("#outP3").show();
        document.getElementById("orderPriceVal").innerHTML = "500 ₽";
        //    $("#orderCurrencyVal").show();
    }

    if (myOrderTime == 4) {
        $("#outP1").hide();
        $("#outP2").hide();
        $("#outP3").hide();
        $("#outP4").show();
        document.getElementById("orderPriceVal").innerHTML = "1500 ₽";
        //   $("#orderCurrencyVal").show();
    }

    quantityChangeFunc();
};

$('#ProductQuantity').on("change", function () {
    quantityChangeFunc();
});

function quantityChangeFunc() {
    //   debugger;
    let myOrderTime = $('#OrderTiming').val();
    let left = $("#outP" + myOrderTime).val();
    //  var AvaliableQuentity = left.split(" ");

    if (left != undefined && left.length > 0 && left != null) {
        var AvaliableQuentity = parseInt(left.replace(/[^0-9.]/g, ""));
    }

    let myPrice;
    if (myOrderTime == 1) {
        myPrice = 500;
    }
    if (myOrderTime == 2) {
        myPrice = 300;
    }
    if (myOrderTime == 3) {
        myPrice = 500;
    }
    if (myOrderTime == 4) {
        myPrice = 1500;
    }

    let mybucketPrice = document.getElementById("orderSize").innerHTML;
    if (mybucketPrice != undefined) {
        if (mybucketPrice == 'XL') {
            mybucketPrice = 3000;
        }
        else if (mybucketPrice == 'L') {
            mybucketPrice = 1000;
        }
        else if (mybucketPrice == 'M') {
            mybucketPrice = 500;
        }
        else if (mybucketPrice == 'S') {
            mybucketPrice = 350;
        }
    }


    if (myPrice == undefined && mybucketPrice != null) {
        myPrice = mybucketPrice;
    }

    let myQuentity = $('#ProductQuantity').val();
    let myQuentityInt = parseInt(myQuentity);
    if (myQuentityInt > AvaliableQuentity) {
        $("#addNewBtn").click(function (e) {
            // custom handling here
            e.preventDefault();
            $('#ProductQuantity').css("backgroundColor", "red");
        });
    }
    if (myQuentityInt <= AvaliableQuentity) {
        // debugger;
        $('#ProductQuantity').css("backgroundColor", "");
        $("#addNewBtn").unbind('click');
    }
    // debugger;

    let PriceToDisplay = myPrice * myQuentity;

    if (PriceToDisplay > 0 && myQuentity == 15 && (myOrderTime == 1 || myOrderTime == 3)) {

        let myOriginalPrice = PriceToDisplay;
        let myDiscount = (PriceToDisplay * 10) / 100;
        let myPriceAfterDiscount = myOriginalPrice - myDiscount;


        document.getElementById("orderPriceVal").innerHTML = "</br>" + "<span>" + myPriceAfterDiscount + ' ₽</span>' + "<span>" + " вместо " + myOriginalPrice + " ₽" + "</span>";
        document.getElementById("orderPriceVal").childNodes[1].style.color = "red";
        document.getElementById("orderPriceVal").childNodes[2].style.color = "gray";


        $("#productPrice").val(myPriceAfterDiscount);
        //     $("#orderCurrencyVal").show();
    }

    else if (PriceToDisplay > 0) {
        //   document.getElementById("prodPriceVal").innerHTML = PriceToDisplay;
        document.getElementById("orderPriceVal").innerHTML = PriceToDisplay + " ₽";
        $("#productPrice").val(PriceToDisplay);
        //    $("#orderCurrencyVal").show();
    }
}

$('#buttonVisit1').on("click", function () {
    console.log(11111111);
    $("#myContentView").show();
    $("#QuantityProd").show();
    $("#CustAddress").hide();
});

$('#buttonVisit2').on("click", function () {
    console.log(11111111);
    $("#myContentView").hide();
    $("#QuantityProd").hide();
    $("#CustAddress").hide();
});

$('#buttonMasterClass').on("click", function () {
    console.log(11111111);
    $("#myContentView").show();
    $("#QuantityProd").show();
    $("#CustAddress").hide();
});

$('#buttonProdBox1').on("click", function () {
    console.log(222222222);
    $("#myContentView").hide();
    $("#QuantityProd").show();
    $("#CustAddress").show();
    $('#ProductQuantity').val("");
    $('#datepicker').val("");
    $('#datepicker2').val("");

    $("#outP1").hide();
    $("#outP2").hide();
    $("#outP3").hide();
    $("#outP4").hide();

});

$('#buttonProdBox2').on("click", function () {
    console.log(222222222);
    $("#myContentView").hide();
    $("#QuantityProd").show();
    $("#CustAddress").show();
});

$('#buttonProdBox3').on("click", function () {
    console.log(222222222);
    $("#myContentView").hide();
    $("#QuantityProd").show();
    $("#CustAddress").show();
});

$('#buttonProdBox4').on("click", function () {
    console.log(222222222);
    $("#myContentView").hide();
    $("#QuantityProd").show();
    $("#CustAddress").show();
});
