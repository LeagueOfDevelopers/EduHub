/**
 * Нужно реализовать сервис рассылки уведомлений
 * по каналам, на которые подписываются пользователи.
 *
 * Для каждого пользователя есть возможность задать способ
 * представления этих уведомлений.
 *
 * Выполненное задание - корректная работа функции App
 */

function App() {

    // создание новго сервиса рассылки
    var notifier = new CreateNotificationService();

    // создание 2 разных каналов
    var channel1 = new notifier.createChannel('channel1');
    var channel2 = new notifier.createChannel('channel2');


    // создание подписчиков

    // Bob хочет получать только последнее сообщение из каждого канала
    var Bob = new CreateReceiver('lastOnly');

    // Tom хочет видеть названия каналов
    var Tom = new CreateReceiver('attachChannelName');

    // Mike не хочет видеть сообщения, которые он уже прочитал
    var Mike = new CreateReceiver('newOnly');

    // подписка на каналы
    Bob.subscribe(channel1);
    Tom.subscribe(channel1);
    Tom.subscribe(channel2);
    Mike.subscribe(channel2);

    // рассылка сообщений по каналам
    notifier.sendToChannel(channel1, 'some message');
    notifier.sendToChannel(channel1, 'another message');
    notifier.sendToChannel(channel2, 'lorem ipsum');
    notifier.sendToChannel(channel2, 'dolor');

    // Получение сообщений, взаимодействие с сообщениями не должно менять состояние сервиса нотификаций
    console.log(Bob.getMessages()); // только 'another message'
    console.log(Tom.getMessages()); // [channel1] : some message , another message | [channel2] : ...
    console.log(Mike.getMessages()); // lorem ipsum , dolor

    notifier.sendToChannel(channel2, 'some new stuff');
    console.log(Mike.getMessages()) // some new stuff
}

App();


function CreateNotificationService() {
    this.createChannel = function (name) {
        this.name = name;
        this.subscribers = [];
        this.messages = [];
    };

    this.sendToChannel = function (channel, message) {
        channel.messages.push(message);
    };
}

function CreateReceiver(callBack) {
    this.callBack = callBack;
    this.subscriber = this;
    this.channels = [];
    this.subscribe = function (channel) {
        channel.subscribers.push(this.subscriber);
        this.channels.push(channel);
    };

    switch (this.callBack) {
        case 'lastOnly':
            this.getMessages = function () {
                var messages = this.channels.map(function (channelItem) {
                    return channelItem.messages[channelItem.messages.length - 1]
                }).join(', ');
                return messages;
            }
            break;

        case 'attachChannelName':
            this.getMessages = function () {
                var messages = this.channels.map(function (channelItem) {
                    return '[' + channelItem.name + ']' + ': ' + channelItem.messages.map(function (messageItem) {
                        return messageItem
                    }).join(', ')
                }).join(' | ');
                return messages;
            }
            break;

        case 'newOnly':
            this.getMessages = function () {
                var messages, oldMessages;

                messages = oldMessages;
                messages = this.channels.map(function (channelItem) {
                    return channelItem.messages.map(function (messageItem) {
                        return messageItem
                    }).join(', ')
                }).join(', ');
                oldMessages = this.channels.map(function (channelItem) {
                    return channelItem.messages = []
                });

                return messages;
            }
            break;
    }
}

