function fetchSymbolData() {
    var symbol = $('#symbol').val();

    $('#result-section').text('');

    if (symbol) {
        $('#result-section').html(`<p>loading ${symbol} data...</p>`);

        $.ajax({
            url: `/api/CryptoPrice/${symbol}`,
            success: res => {
                $('#result-section').html('<ul></ul>');

                if (Array.isArray(res)) {
                    for (var i = 0; i < res.length; i++) {
                        let { baseCurrency, price } = res[i];
                        $('#result-section ul').append(`<li>${symbol}_${baseCurrency} : ${price}</li>`)
                    }

                    $('#result-section ul li').commify();
                }
            },
            error: res => {
                console.log(res);
                $('#result-section').html(`<p class="text-danger">error on loading ${symbol}!</p>`);
            }
        })
    }
}

$(() => {
    $('#symbol').on('change', fetchSymbolData)
})