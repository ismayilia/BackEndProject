$(document).ready(function () {

    $(document).on("click", ".product-section .product-item .product-add a", function (e) {

        e.preventDefault();
        let id = parseInt($(this).closest(".product-item").attr("data-productId"));

        let count = $(".basket-count").text();
        console.log(count);

        $.ajax({
            url: `home/addtocart?id=${id}`,
            type: "Post",
            success: function (res) {

                count++;

                $(".basket-count").text(count);

            }
        })



    })

    $(document).on("click", ".delete-basket-item", function (e) {
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `cart/deletedatafrombasket?id=${id}`,
            type: "Post",
            success: function (res) {


                $(".basket-count").text(res.count);
                $(e.target).closest("tr").remove();
                $(".grand-total h1 span").text(res.grandTotal);

                if (res.count === 0) {
                    $(".alert-basket-empty").removeClass("d-none");
                    $(".basket-table").addClass("d-none");
                }
            }
        })
        

    })

    $(document).on("click", ".basket-table .fa-plus", function (e) {

        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket-count").text();
        $.ajax({

            url: `cart/plusicon?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).prev().text(res.countItem)
                $(".grand-total h1 span").text(res.basketGrandTotal);
                $(e.target).parent().next().next().children().text(res.productGrandTotal)
                count++;

                $(".basket-count").text(count);
            }
        })

    })


    $(document).on("click", ".basket-table .fa-minus", function (e) {

        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket - count").text();
        let a = 0;

        $.ajax({

            url: `cart/minusicon?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).next().text(res.countItem)
                $(".grand-total h1 span").text(res.basketGrandTotal);
                $(e.target).parent().next().next().children().text(res.productGrandTotal)
                $(".count-Basket").text(res.countBasket)

            }
        })

    })





})