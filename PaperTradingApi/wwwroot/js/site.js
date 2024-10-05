import WebSocketSingleton from './ws.js'
const homeStocks = document.querySelectorAll('.stock');
const homeToken = document.querySelector('#FinnhubToken').value;
const wsInstance = new WebSocketSingleton(`wss://ws.finnhub.io?token=${homeToken}`)
const sharedSocket = wsInstance.getConnection();
const homeStockList = {};
homeStocks.forEach(div => {
    homeStockList[div.querySelector('.stock-title').textContent] = div.querySelector('.price');
});
sharedSocket.addEventListener('open', function (event) {
    for (let stock in homeStockList) {
        sharedSocket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stock }))
    }
});

sharedSocket.addEventListener('message', function (event) {
    if (event.data.type === "error") {
        return
    }
    var eventData = JSON.parse(event.data);
    if (eventData) {
        if (eventData.data) {
            var updatedPrice = JSON.parse(event.data).data[0].p;
            var symbol = JSON.parse(event.data).data[0].s;
            homeStockList[symbol].textContent = updatedPrice.toFixed(2);
        }
    }
});
window.onbeforeunload = function () {
    for (let stock in homeStockList) {
        sharedSocket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': stock }))
    }
    sharedSocket.close();
}