webpackJsonp([10],{"./app/components/Chat/index.js":function(e,t,n){"use strict";function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function s(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function i(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function a(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function u(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function l(e){return{sendMessage:function(t,n){return e(Object(v.W)(t,n))}}}function c(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function p(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function f(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}Object.defineProperty(t,"__esModule",{value:!0});var h=n("./node_modules/react/react.js"),d=n.n(h),m=n("./node_modules/react-dom/index.js"),y=n.n(m),b=(n("./node_modules/prop-types/index.js"),n("./node_modules/reselect/es/index.js")),g=n("./node_modules/react-redux/es/index.js"),v=n("./app/containers/GroupPage/actions.js"),w=n("./node_modules/antd/lib/index.js"),O=n("./app/globalJS.js"),_=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var s=t&&t.defaultProps,i=arguments.length-3;if(n||0===i||(n={}),n&&s)for(var a in s)void 0===n[a]&&(n[a]=s[a]);else n||(n=s||{});if(1===i)n.children=r;else if(i>1){for(var u=Array(i),l=0;l<i;l++)u[l]=arguments[l+3];n.children=u}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),j=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),I=function(e){function t(e){return o(this,t),r(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return s(t,e),j(t,[{key:"render",value:function(){return this.props.message?_("li",{className:"message "+(Object(O.d)(localStorage.getItem("token")).UserId===this.props.message.senderId?"right":"left")},void 0,Object(O.d)(localStorage.getItem("token")).UserId!==this.props.message.senderId&&_("span",{style:{opacity:.8}},void 0,this.props.message.senderId),_("p",{style:{margin:"6px 0"}},void 0,this.props.message.text),_("div",{style:{textAlign:"right",fontSize:14,opacity:.5}},void 0,this.props.message.sentOn)):null}}]),t}(d.a.Component),S=I,P=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var s=t&&t.defaultProps,i=arguments.length-3;if(n||0===i||(n={}),n&&s)for(var a in s)void 0===n[a]&&(n[a]=s[a]);else n||(n=s||{});if(1===i)n.children=r;else if(i>1){for(var u=Array(i),l=0;l<i;l++)u[l]=arguments[l+3];n.children=u}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),E=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),k=P("div",{className:"header"},void 0,"Чат"),M=function(e){function t(e){i(this,t);var n=a(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.state={messages:[],showIsInGroupError:!1},n}return u(t,e),E(t,[{key:"componentDidUpdate",value:function(e,t){(e.chat&&this.props.chat&&e.chat.length!==this.props.chat.length||!e.chat&&this.props.chat)&&this.scrollToBottom()}},{key:"scrollToBottom",value:function(){y.a.findDOMNode(this.chat).scrollTop=y.a.findDOMNode(this.chat).scrollHeight}},{key:"submitMessage",value:function(e){e.preventDefault(),this.props.isInGroup?this.setState({showIsInGroupError:!1}):this.setState({showIsInGroupError:!0}),""!==y.a.findDOMNode(this.msgInput).value&&this.props.isInGroup&&("true"===localStorage.getItem("withoutServer")?this.setState({messages:this.state.messages.concat([{id:Math.random(),username:localStorage.getItem("name"),content:y.a.findDOMNode(this.msgInput).value,time:(new Date).getHours()+":"+((new Date).getMinutes()<10?"0":"")+(new Date).getMinutes()}])}):this.props.sendMessage(this.props.groupId,y.a.findDOMNode(this.msgInput).value)),y.a.findDOMNode(this.msgInput).value=""}},{key:"render",value:function(){var e=this;return P("div",{className:"chatroom"},void 0,k,d.a.createElement("ul",{className:"chat",ref:function(t){return e.chat=t}},"true"===localStorage.getItem("withoutServer")?this.state.messages.map(function(e){return P(S,{message:e,user:localStorage.getItem("name")},e.id)}):this.props.chat?this.props.chat.map(function(e){return P(S,{message:e},e.id)}):null),P("form",{className:"input",onSubmit:function(t){return e.submitMessage(t)}},void 0,d.a.createElement(w.Input,{size:"large",ref:function(t){return e.msgInput=t},placeholder:"Введите сообщение"})),this.state.showIsInGroupError?P("div",{style:{color:"red"}},void 0,"Вы должны вступить в группу"):"")}}]),t}(d.a.Component),x=Object(b.b)({}),D=Object(g.b)(x,l)(M),N=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var s=t&&t.defaultProps,i=arguments.length-3;if(n||0===i||(n={}),n&&s)for(var a in s)void 0===n[a]&&(n[a]=s[a]);else n||(n=s||{});if(1===i)n.children=r;else if(i>1){for(var u=Array(i),l=0;l<i;l++)u[l]=arguments[l+3];n.children=u}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),T=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),C=function(e){function t(){return c(this,t),p(this,(t.__proto__||Object.getPrototypeOf(t)).apply(this,arguments))}return f(t,e),T(t,[{key:"render",value:function(){return N("div",{},void 0,d.a.createElement(D,this.props))}}]),t}(d.a.Component);t.default=C}});