class WebSocketSingleton {
    constructor(url) {
        if (!WebSocketSingleton.instance) {
            this.connection = new WebSocket(url);
            WebSocketSingleton.instance = this;
        }
        return WebSocketSingleton.instance;
    }
    getConnection() {
        return this.connection;
    }
}
export default WebSocketSingleton;