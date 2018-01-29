webpackJsonp([0],{"./app/components/InviteCard/index.js":function(e,t,n){"use strict";function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function a(e){return{acceptInvitation:function(t,n,o){return e(Object(g.a)(t,n,o))},declineInvitation:function(t,n,o){return e(Object(g.a)(t,n,o))}}}var s=n("./node_modules/react/react.js"),c=n.n(s),u=n("./node_modules/prop-types/index.js"),p=(n.n(u),n("./node_modules/react-redux/es/index.js")),l=n("./node_modules/reselect/es/index.js"),f=n("./node_modules/redux/es/index.js"),d=n("./app/utils/injectSaga.js"),y=n("./app/utils/injectReducer.js"),b=n("./app/containers/NotificationPage/reducer.js"),v=n("./app/containers/NotificationPage/saga.js"),g=n("./app/containers/NotificationPage/actions.js"),h=n("./app/globalJS.js"),m=n("./node_modules/antd/lib/index.js"),j=(n.n(m),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var i=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&i)for(var s in i)void 0===n[s]&&(n[s]=i[s]);else n||(n=i||{});if(1===a)n.children=r;else if(a>1){for(var c=Array(a),u=0;u<a;u++)c[u]=arguments[u+3];n.children=c}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}()),_=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),w=j("div",{className:"readed-btn"}),x=j("div",{className:"not-readed-btn"}),O=function(e){function t(e){o(this,t);var n=r(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.tryAccept=n.tryAccept.bind(n),n.tryDecline=n.tryDecline.bind(n),n}return i(t,e),_(t,[{key:"tryAccept",value:function(){"true"!==localStorage.getItem("without_server")&&this.props.acceptInvitation(this.props.groupId,this.props.id,"Accepted"),m.message.success("Приглашение принято")}},{key:"tryDecline",value:function(){"true"!==localStorage.getItem("without_server")&&this.props.declineInvitation(this.props.groupId,this.props.id,"Declined"),m.message.success("Приглашение отклонено")}},{key:"render",value:function(){return j(m.Card,{hoverable:!0,className:"notify-card",style:{width:"100%",cursor:"default"},bodyStyle:{padding:"14px 20px 0 20px"}},void 0,this.props.readed?w:x,j(m.Row,{style:{marginBottom:12}},void 0,j(m.Col,{span:12},void 0,j("span",{style:{fontSize:14,opacity:.9}},void 0,this.props.fromUser)),j(m.Col,{span:12,style:{textAlign:"right"}},void 0,j("span",{style:{fontSize:14,opacity:.7}},void 0,this.props.date))),j(m.Row,{},void 0,j(m.Col,{xs:{span:24},sm:{span:12},style:{marginBottom:10}},void 0,j("span",{style:{wordWrap:"break-word"}},void 0,"Вас пригласили в группу ",this.props.groupId,' на роль "',Object(h.b)(this.props.suggestedRole),'"')),j(m.Col,{xs:{span:24},sm:{span:12},style:{textAlign:"right"}},void 0,j(m.Button,{type:"primary",style:{marginRight:12,marginBottom:14},onClick:this.tryAccept},void 0,"Принять"),j(m.Button,{onClick:this.tryDecline},void 0,"Отклонить"))))}}]),t}(c.a.PureComponent),S=Object(l.b)({}),P=Object(p.b)(S,a),N=Object(y.a)({key:"notificationPage",reducer:b.a}),I=Object(d.a)({key:"notificationPage",saga:v.a});t.a=Object(f.c)(N,I,P)(O)},"./app/components/NotifyCard/index.js":function(e,t,n){"use strict";function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}var a=n("./node_modules/react/react.js"),s=n.n(a),c=n("./node_modules/prop-types/index.js"),u=(n.n(c),n("./node_modules/antd/lib/index.js")),p=(n.n(u),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var i=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&i)for(var s in i)void 0===n[s]&&(n[s]=i[s]);else n||(n=i||{});if(1===a)n.children=r;else if(a>1){for(var c=Array(a),u=0;u<a;u++)c[u]=arguments[u+3];n.children=c}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}()),l=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),f=p("div",{className:"readed-btn"}),d=p("div",{className:"not-readed-btn"}),y=function(e){function t(e){return o(this,t),r(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return i(t,e),l(t,[{key:"render",value:function(){return p(u.Card,{hoverable:!0,className:"notify-card",style:{width:"100%",cursor:"default",position:"relative",overflow:"visible"},bodyStyle:{padding:"14px 20px"}},void 0,this.props.readed?f:d,p(u.Row,{style:{marginBottom:12}},void 0,p(u.Col,{span:12},void 0,p("span",{style:{fontSize:14,opacity:.9}},void 0,this.props.fromUser)),p(u.Col,{span:12,style:{textAlign:"right"}},void 0,p("span",{style:{fontSize:14,opacity:.7}},void 0,this.props.date))),p(u.Row,{},void 0,p("span",{style:{wordWrap:"break-word"}},void 0,this.props.text)))}}]),t}(s.a.PureComponent);t.a=y},"./app/containers/NotificationPage/actions.js":function(e,t,n){"use strict";function o(e,t,n){return{type:a.b,groupId:e,invitationId:t,status:n}}function r(){return{type:a.c}}function i(e){return{type:a.a,error:e}}t.a=o,t.c=r,t.b=i;var a=n("./app/containers/NotificationPage/constants.js")},"./app/containers/NotificationPage/constants.js":function(e,t,n){"use strict";n.d(t,"b",function(){return o}),n.d(t,"c",function(){return r}),n.d(t,"a",function(){return i});var o="CHANGE_INVITATION_STATUS_START",r="CHANGE_INVITATION_STATUS_SUCCESS",i="CHANGE_INVITATION_STATUS_FAILED"},"./app/containers/NotificationPage/index.js":function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),function(e){function o(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function a(e){return{dispatch:e}}n.d(t,"NotificationPage",function(){return S});var s=n("./node_modules/react/react.js"),c=n.n(s),u=n("./node_modules/prop-types/index.js"),p=(n.n(u),n("./node_modules/react-redux/es/index.js")),l=n("./node_modules/reselect/es/index.js"),f=n("./node_modules/redux/es/index.js"),d=n("./app/utils/injectSaga.js"),y=n("./app/utils/injectReducer.js"),b=(n("./app/containers/NotificationPage/selectors.js"),n("./app/containers/NotificationPage/reducer.js")),v=n("./app/containers/NotificationPage/saga.js"),g=n("./node_modules/antd/lib/index.js"),h=(n.n(g),n("./app/components/NotifyCard/index.js")),m=n("./app/components/InviteCard/index.js"),j=n("./app/config.js"),_=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var o in n)Object.prototype.hasOwnProperty.call(n,o)&&(e[o]=n[o])}return e},w=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,r){var i=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&i)for(var s in i)void 0===n[s]&&(n[s]=i[s]);else n||(n=i||{});if(1===a)n.children=r;else if(a>1){for(var c=Array(a),u=0;u<a;u++)c[u]=arguments[u+3];n.children=c}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),x=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),O=g.Tabs.TabPane,S=function(t){function n(e){o(this,n);var t=r(this,(n.__proto__||Object.getPrototypeOf(n)).call(this,e));return t.notifies=[{fromUser:"Имя отправителя",date:(new Date).toDateString(),text:"Текст уведомления",readed:!1},{fromUser:"Имя отправителя",date:(new Date).toDateString(),text:"Текст уведомления",readed:!0},{fromUser:"Имя отправителя",date:(new Date).toDateString(),text:"Текст уведомления",readed:!0}],t.invites=[{fromUser:"Имя отправителя",date:(new Date).toDateString(),groupId:"32478643981654",suggestedRole:"Участник",readed:!1},{fromUser:"Имя отправителя",date:(new Date).toDateString(),groupId:"dr32847363274",suggestedRole:"Участник",readed:!0}],t.state={notifies:[],invites:[]},t}return i(n,t),x(n,[{key:"componentDidMount",value:function(){var t=this;"true"===localStorage.getItem("without_server")?this.setState({notifies:this.notifies,invites:this.invites}):(e(j.a.API_BASE_URL+"/user/profile/notifies",{headers:{Authorization:"Bearer "+localStorage.getItem("token")}}).then(function(e){return e.json()}).then(function(e){t.setState({notifies:e})}).catch(function(e){return e}),e(j.a.API_BASE_URL+"/user/profile/invitations",{headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).then(function(e){return e.json()}).then(function(e){t.setState({invites:e.invitations})}).catch(function(e){return e}))}},{key:"render",value:function(){return w("div",{},void 0,w(g.Row,{className:"notify-tabs"},void 0,w(g.Col,{xs:{span:22,offset:1},sm:{span:20,offset:2},lg:{span:12,offset:6}},void 0,w(g.Tabs,{defaultActiveKey:"1",style:{margin:"30px 0"}},void 0,w(O,{tab:"Уведомления",style:{margin:"30px 0"}},"1",w("div",{},void 0,this.state.notifies.reverse().map(function(e){return c.a.createElement(h.a,_({key:e.id},e))}))),w(O,{tab:"Приглашения",style:{margin:"30px 0"}},"2",w("div",{},void 0,this.state.invites.reverse().map(function(e){return c.a.createElement(m.a,_({key:e.id},e))})))))))}}]),n}(c.a.Component),P=Object(l.b)({}),N=Object(p.b)(P,a),I=Object(y.a)({key:"notificationPage",reducer:b.a}),A=Object(d.a)({key:"notificationPage",saga:v.a});t.default=Object(f.c)(I,A,N)(S)}.call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))},"./app/containers/NotificationPage/reducer.js":function(e,t,n){"use strict";function o(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:a;switch(arguments[1].type){case i.b:return e.set("pending",!0);case i.c:return e.set("pending",!1).set("error",!1);case i.a:return e.set("pending",!1).set("error",!0);default:return e}}var r=n("./node_modules/immutable/dist/immutable.js"),i=(n.n(r),n("./app/containers/NotificationPage/constants.js")),a=Object(r.fromJS)({pending:!1,error:!1});t.a=o},"./app/containers/NotificationPage/saga.js":function(e,t,n){"use strict";(function(e){function o(e){return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(a.a)(r,e.groupId,e.invitationId,e.status);case 3:return t.next=5,Object(a.b)(Object(u.c)());case 5:t.next=11;break;case 7:return t.prev=7,t.t0=t.catch(0),t.next=11,Object(a.b)(Object(u.b)(t.t0));case 11:case"end":return t.stop()}},p,this,[[0,7]])}function r(t,n,o){return e(s.a.API_BASE_URL+"/group/"+t+"/member",{method:"PUT",headers:{Accept:"application/json","Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({invitationId:n,status:o})}).catch(function(e){return e})}function i(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(a.c)(c.b,o);case 2:case"end":return e.stop()}},l,this)}t.a=i;var a=n("./node_modules/redux-saga/es/effects.js"),s=n("./app/config.js"),c=n("./app/containers/NotificationPage/constants.js"),u=n("./app/containers/NotificationPage/actions.js"),p=regeneratorRuntime.mark(o),l=regeneratorRuntime.mark(i)}).call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))},"./app/containers/NotificationPage/selectors.js":function(e,t,n){"use strict";n("./node_modules/reselect/es/index.js")},"./app/globalJS.js":function(e,t,n){"use strict";function o(e){var t=e.split(".")[1],n=t.replace("-","+").replace("_","/");return JSON.parse(window.atob(n))}function r(e){switch(e){case 0:return"Обычный пользователь";case 1:return"Участник";case 2:return"Учитель";case 3:return"Создатель"}}function i(e){switch(e){case 0:return"Лекция";case 1:return"Семинар";case 2:return"Мастер-класс"}}t.c=o,t.b=r,t.a=i}});