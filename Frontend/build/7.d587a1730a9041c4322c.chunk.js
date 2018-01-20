webpackJsonp([7],{"./app/components/Chat/index.js":function(e,t,n){"use strict";function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function s(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function a(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function u(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function l(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function c(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function f(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}Object.defineProperty(t,"__esModule",{value:!0});var p=n("./node_modules/react/react.js"),y=n.n(p),h=n("./node_modules/react-dom/index.js"),m=n.n(h),d=(n("./node_modules/prop-types/index.js"),n("./node_modules/antd/lib/index.js")),b=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var i=t&&t.defaultProps,s=arguments.length-3;if(n||0===s||(n={}),n&&i)for(var a in i)void 0===n[a]&&(n[a]=i[a]);else n||(n=i||{});if(1===s)n.children=r;else if(s>1){for(var u=Array(s),l=0;l<s;l++)u[l]=arguments[l+3];n.children=u}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),v=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),g=function(e){function t(e){return o(this,t),r(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return i(t,e),v(t,[{key:"render",value:function(){return b("li",{className:"message "+(this.props.user===this.props.message.username?"right":"left")},void 0,this.props.user!==this.props.message.username&&b("span",{style:{opacity:.8}},void 0,this.props.message.username),b("p",{style:{margin:"6px 0"}},void 0,this.props.message.content),b("div",{style:{textAlign:"right",fontSize:14,opacity:.5}},void 0,this.props.message.time))}}]),t}(y.a.Component),w=g,_=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var i=t&&t.defaultProps,s=arguments.length-3;if(n||0===s||(n={}),n&&i)for(var a in i)void 0===n[a]&&(n[a]=i[a]);else n||(n=i||{});if(1===s)n.children=r;else if(s>1){for(var u=Array(s),l=0;l<s;l++)u[l]=arguments[l+3];n.children=u}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),O=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),j=_("div",{className:"header"},void 0,"Чат"),S=function(e){function t(e){s(this,t);var n=a(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.state={messages:[],showIsInGroupError:!1},n}return u(t,e),O(t,[{key:"componentDidMount",value:function(){this.scrollToBottom()}},{key:"componentDidUpdate",value:function(){this.scrollToBottom()}},{key:"scrollToBottom",value:function(){m.a.findDOMNode(this.chat).scrollTop=m.a.findDOMNode(this.chat).scrollHeight}},{key:"submitMessage",value:function(e){e.preventDefault(),this.props.isInGroup?this.setState({showIsInGroupError:!1}):this.setState({showIsInGroupError:!0}),""!==m.a.findDOMNode(this.msgInput).value&&this.props.isInGroup&&this.setState({messages:this.state.messages.concat([{username:localStorage.getItem("name"),content:m.a.findDOMNode(this.msgInput).value,time:(new Date).getHours()+":"+((new Date).getMinutes()<10?"0":"")+(new Date).getMinutes()}])}),m.a.findDOMNode(this.msgInput).value=""}},{key:"render",value:function(){var e=this;return _("div",{className:"chatroom"},void 0,j,y.a.createElement("ul",{className:"chat",ref:function(t){return e.chat=t}},this.state.messages.map(function(e){return _(w,{message:e,user:localStorage.getItem("name")})})),_("form",{className:"input",onSubmit:function(t){return e.submitMessage(t)}},void 0,y.a.createElement(d.Input,{size:"large",ref:function(t){return e.msgInput=t},placeholder:"Введите сообщение"})),this.state.showIsInGroupError?_("div",{style:{color:"red"}},void 0,"Вы должны вступить в группу"):"")}}]),t}(y.a.Component),I=S,E=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var i=t&&t.defaultProps,s=arguments.length-3;if(n||0===s||(n={}),n&&i)for(var a in i)void 0===n[a]&&(n[a]=i[a]);else n||(n=i||{});if(1===s)n.children=r;else if(s>1){for(var u=Array(s),l=0;l<s;l++)u[l]=arguments[l+3];n.children=u}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),P=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),k=function(e){function t(){return l(this,t),c(this,(t.__proto__||Object.getPrototypeOf(t)).apply(this,arguments))}return f(t,e),P(t,[{key:"render",value:function(){return E("div",{},void 0,y.a.createElement(I,this.props))}}]),t}(y.a.Component);t.default=k}});