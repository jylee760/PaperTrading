import WebSocketSingleton from './ws.js'
const profileStocks = document.querySelectorAll('.individualStock');
const profileToken = document.querySelector('#FinnhubToken').value;
const wsInstance = new WebSocketSingleton(`wss://ws.finnhub.io?token=${profileToken}`)
const sharedSocket = wsInstance.getConnection();
const profileStockList = {};
profileStocks.forEach(tr => {
    profileStockList[tr.querySelector('.stock-title').textContent] = [tr.querySelector('.price'),tr.querySelector('.currentPrice')];
});
sharedSocket.addEventListener('open', function (event) {
    for (let stock in profileStockList) {
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
            profileStockList[symbol][0].textContent = updatedPrice.toFixed(2);
            profileStockList[symbol][1].value = updatedPrice.toFixed(2);
            var event = new Event('input', {
                cancelable=true
            });
            profileStockList[symbol][1].dispatchEvent(event);
        }
    }
});
window.onbeforeunload = function () {
    for (let stock in profileStockList) {
        sharedSocket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': stock }))
    }
    sharedSocket.close();
}