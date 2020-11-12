window.ShowAlert = function (message, button) {
    alert(message);
    button.style.backgroundColor = "yellow";

    //ezt a blazor adja, így lehet bármilyen static ( [JSInvokable] ) fv-t hívni
    //az első paraméter a teljes projekt neve, nem a pontos namespace
    //a második paraméter a függvény neve
    // a további paramétereket a függvény kapja meg
    DotNet.invokeMethodAsync('BlazorApp1', 'GetHelloMessage', "param")
        .then(message => {
            console.log(message);
        });

    return "helloka"
}

//így hívhatunk instance-hoz tartozó c# függvényt
window.WriteCSharpMessageToConsole = (dotnetHelper) => {
    dotnetHelper.invokeMethodAsync('GetHelloMessage')
        .then(message => console.log(message));
}