webpackJsonp([8],{"./app/components/Chat/index.js":function(e,t,o){"use strict";function n(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function s(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function i(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function a(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function u(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function l(e){return{sendMessage:function(t,o){return e(Object(v._5)(t,o))},getCurrentChat:function(t){return e(Object(v.N)(t))}}}function c(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function p(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function f(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}Object.defineProperty(t,"__esModule",{value:!0});var d=o("./node_modules/react/react.js"),h=o.n(d),m=o("./node_modules/react-dom/index.js"),g=o.n(m),y=(o("./node_modules/prop-types/index.js"),o("./node_modules/reselect/es/index.js")),b=o("./node_modules/react-redux/es/index.js"),v=o("./app/containers/GroupPage/actions.js"),w=o("./app/globalJS.js"),O=o("./node_modules/antd/lib/index.js"),I=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,o,n,r){var s=t&&t.defaultProps,i=arguments.length-3;if(o||0===i||(o={}),o&&s)for(var a in s)void 0===o[a]&&(o[a]=s[a]);else o||(o=s||{});if(1===i)o.children=r;else if(i>1){for(var u=Array(i),l=0;l<i;l++)u[l]=arguments[l+3];o.children=u}return{$$typeof:e,type:t,key:void 0===n?null:""+n,ref:null,props:o,_owner:null}}}(),S=function(){function e(e,t){for(var o=0;o<t.length;o++){var n=t[o];n.enumerable=n.enumerable||!1,n.configurable=!0,"value"in n&&(n.writable=!0),Object.defineProperty(e,n.key,n)}}return function(t,o,n){return o&&e(t.prototype,o),n&&e(t,n),t}}(),_=function(e){function t(e){return n(this,t),r(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return s(t,e),S(t,[{key:"render",value:function(){return this.props.message?I("li",{className:"message "+(Object(w.g)(localStorage.getItem("token")).UserId==this.props.message.senderId?"right":"left")},void 0,Object(w.g)(localStorage.getItem("token")).UserId!=this.props.message.senderId&&I("span",{style:{opacity:.8}},void 0,this.props.message.senderName),I("p",{style:{margin:"6px 0"}},void 0,this.props.message.text),I("div",{style:{textAlign:"left",fontSize:12,opacity:.5,marginBottom:6}},void 0,new Date(this.props.message.sentOn).getHours()+":"+(new Date(this.props.message.sentOn).getMinutes()<10?"0"+new Date(this.props.message.sentOn).getMinutes():new Date(this.props.message.sentOn).getMinutes()))):null}}]),t}(h.a.Component),j=_,k=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,o,n,r){var s=t&&t.defaultProps,i=arguments.length-3;if(o||0===i||(o={}),o&&s)for(var a in s)void 0===o[a]&&(o[a]=s[a]);else o||(o=s||{});if(1===i)o.children=r;else if(i>1){for(var u=Array(i),l=0;l<i;l++)u[l]=arguments[l+3];o.children=u}return{$$typeof:e,type:t,key:void 0===n?null:""+n,ref:null,props:o,_owner:null}}}(),M=function(){function e(e,t){for(var o=0;o<t.length;o++){var n=t[o];n.enumerable=n.enumerable||!1,n.configurable=!0,"value"in n&&(n.writable=!0),Object.defineProperty(e,n.key,n)}}return function(t,o,n){return o&&e(t.prototype,o),n&&e(t,n),t}}(),x=k("div",{className:"header"},void 0,"Чат"),N=function(e){function t(e){i(this,t);var o=a(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e)),n="ws://85.143.104.47:2411/api/sockets/creation?token="+localStorage.getItem("token");return localStorage.getItem("token")&&(o.socket=new WebSocket(n)),o.state={messages:[],showIsInGroupError:!1},o}return u(t,e),M(t,[{key:"componentDidMount",value:function(){localStorage.getItem("token")&&this.connectSocket(this.socket),this.scrollToBottom()}},{key:"connectSocket",value:function(e){var t=this;e.onopen=function(e){console.log("opened connection")},e.onclose=function(e){console.log("closed connection")},e.onmessage=function(e){t.props.getCurrentChat(t.props.groupId),console.log("message received "+JSON.stringify(JSON.parse(e.data)))},e.onerror=function(e){console.log("error "+JSON.stringify(e))}}},{key:"componentDidUpdate",value:function(e,t){(e.chat&&this.props.chat&&e.chat.length!==this.props.chat.length||!e.chat&&this.props.chat)&&this.scrollToBottom(),e.isInGroup!==this.props.isInGroup&&this.props.isInGroup&&this.setState({showIsInGroupError:!1})}},{key:"componentWillUnmount",value:function(){localStorage.getItem("token")&&this.socket.close()}},{key:"scrollToBottom",value:function(){g.a.findDOMNode(this.chat).scrollTop=g.a.findDOMNode(this.chat).scrollHeight}},{key:"submitMessage",value:function(e){e&&e.preventDefault(),this.props.isInGroup?this.setState({showIsInGroupError:!1}):this.setState({showIsInGroupError:!0}),""!==g.a.findDOMNode(this.msgInput).value&&this.props.isInGroup&&("true"===localStorage.getItem("withoutServer")?this.setState({messages:this.state.messages.concat([{id:Math.random(),username:localStorage.getItem("name"),text:g.a.findDOMNode(this.msgInput).value,time:(new Date).getHours()+":"+((new Date).getMinutes()<10?"0":"")+(new Date).getMinutes()}])}):this.props.sendMessage(this.props.groupId,g.a.findDOMNode(this.msgInput).value)),g.a.findDOMNode(this.msgInput).value="",g.a.findDOMNode(this.msgInput).focus()}},{key:"render",value:function(){var e=this;return k("div",{},void 0,k("div",{className:"chatroom"},void 0,x,k("div",{style:{overflowX:"hidden"}},void 0,h.a.createElement("ul",{className:"chat",ref:function(t){return e.chat=t}},"true"===localStorage.getItem("withoutServer")?this.state.messages.map(function(e){return k(j,{message:e,user:localStorage.getItem("name")},e.id)}):this.props.chat?this.props.chat.map(function(e,t){return k(j,{message:e},e.id?e.id:Math.random())}):[].map(function(e,t){return k(j,{},t)})),k("form",{className:"input",onSubmit:function(t){return e.submitMessage(t)}},void 0,k(O.Row,{type:"flex"},void 0,k(O.Col,{style:{width:"calc(100% - 44px)"}},void 0,h.a.createElement(O.Input,{size:"large",style:{width:"100%"},ref:function(t){return e.msgInput=t},placeholder:"Введите сообщение"})),k(O.Col,{style:{display:"flex",height:40,alignItems:"center",justifyContent:"flex-end",minWidth:24,marginRight:20}},void 0,k("img",{src:o("./app/images/send-blue.svg"),onClick:function(){return e.submitMessage()},className:"send-msg-btn"})))))),this.state.showIsInGroupError?k("div",{style:{color:"red",marginTop:4}},void 0,"Вы должны вступить в группу"):"")}}]),t}(h.a.Component),D=Object(y.b)({}),E=Object(b.b)(D,l)(N),P=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,o,n,r){var s=t&&t.defaultProps,i=arguments.length-3;if(o||0===i||(o={}),o&&s)for(var a in s)void 0===o[a]&&(o[a]=s[a]);else o||(o=s||{});if(1===i)o.children=r;else if(i>1){for(var u=Array(i),l=0;l<i;l++)u[l]=arguments[l+3];o.children=u}return{$$typeof:e,type:t,key:void 0===n?null:""+n,ref:null,props:o,_owner:null}}}(),C=function(){function e(e,t){for(var o=0;o<t.length;o++){var n=t[o];n.enumerable=n.enumerable||!1,n.configurable=!0,"value"in n&&(n.writable=!0),Object.defineProperty(e,n.key,n)}}return function(t,o,n){return o&&e(t.prototype,o),n&&e(t,n),t}}(),G=function(e){function t(){return c(this,t),p(this,(t.__proto__||Object.getPrototypeOf(t)).apply(this,arguments))}return f(t,e),C(t,[{key:"render",value:function(){return P("div",{},void 0,h.a.createElement(E,this.props))}}]),t}(h.a.Component);t.default=G},"./app/images/send-blue.svg":function(e,t,o){e.exports=o.p+"5e12684b03ab6e7d7bc758f699885628.svg"}});