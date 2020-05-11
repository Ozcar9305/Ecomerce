<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="WebApplication.ForzaUltra.Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <main>
    <!--Carousel Wrapper-->
    <div id="carousel-example-1z" class="carousel slide carousel-fade pt-4" data-ride="carousel">

        <!--Slides-->
        <div class="carousel-inner" role="listbox">

            <!--First slide-->
            <div class="carousel-item active">
                <div class="view" style="background-image: url('../Images/ForzaUltra/Site/fu_banner.jpg'); background-repeat: no-repeat; background-size: cover;">

                    <!-- Mask & flexbox options-->
                    <div class="mask rgba-black-strong d-flex justify-content-center align-items-center">

                        <!-- Content -->
                        <div class="text-center white-text mx-5 wow fadeIn">
                           

                            <img src="../Images/ForzaUltra/Site/fu_logo.png" width="200" height="170"/>
                        </div>
                        <!-- Content -->

                    </div>
                    <!-- Mask & flexbox options-->

                </div>
            </div>
            <!--/First slide-->

            <!--Second slide-->
            <div class="carousel-item">
                
            </div>
            <!--/Second slide-->

            <!--Third slide-->
            <!--/Third slide-->

        </div>
        <!--/.Slides-->

        <!--Controls-->
        <a class="carousel-control-prev" href="#carousel-example-1z" role="button" data-slide="prev">
            <%--<span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>--%>
        </a>
        <a class="carousel-control-next" href="#carousel-example-1z" role="button" data-slide="next">
            <%--<span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>--%>
        </a>
        <!--/.Controls-->

    </div>
    <!--/.Carousel Wrapper-->
    </main>
    <script type="text/javascript">
        (function (){
            $.ajax({
                type: "POST",
                url: "Store.aspx/GetStoreGetList",
                data: "",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    console.log(response);
                },
                failure: function () {
                    alert("Sorry,there is a error!");
                }
            });
        })();
    </script>
</asp:Content>
