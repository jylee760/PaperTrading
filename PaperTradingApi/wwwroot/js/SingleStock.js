import WebSocketSingleton from './ws.js'
const Stock = document.querySelector('@stockTicker').value;
const Token = document.querySelector('#FinnhubToken').value;
const wsInstance = new WebSocketSingleton(`wss://ws.finnhub.io?token=${Token}`)
const sharedSocket = wsInstance.getConnection();
const StockAttributes = {};
StockAttributes[Stock] = [document.querySelector('#Price').document.querySelector('#CurrentPrice')]

sharedSocket.addEventListener('open', function (event) {
        sharedSocket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': Stock }))
    
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
            StockAttributes[Stock][0].textContent = updatedPrice.toFixed(2);
            StockAttributes[Stock][1].value = updatedPrice.toFixed(2);
        }
    }
});
window.onbeforeunload = function () {
        sharedSocket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': Stock }))
    
    sharedSocket.close();
}