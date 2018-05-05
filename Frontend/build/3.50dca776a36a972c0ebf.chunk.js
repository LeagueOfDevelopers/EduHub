webpackJsonp([3],{"./app/containers/NotificationPage/actions.js":function(e,t,n){"use strict";function o(e,t){return{type:h.b,invitationId:e,status:t}}function i(e,t){return{type:h.c,groupId:e,status:t}}function r(e){return{type:h.a,error:e}}function a(){return{type:h.k}}function s(e){return{type:h.l,notifies:e}}function l(e){return{type:h.j,error:e}}function c(){return{type:h.h}}function u(e){return{type:h.i,invites:e}}function d(e){return{type:h.g,error:e}}function p(e){return{type:h.e,link:e}}function v(e){return{type:h.f,file:e}}function f(e){return{type:h.d,link:e}}t.a=o,t.c=i,t.b=r,t.j=a,t.l=s,t.k=l,t.g=c,t.i=u,t.h=d,t.d=p,t.f=v,t.e=f;var h=n("./app/containers/NotificationPage/constants.js")},"./app/containers/NotificationPage/constants.js":function(e,t,n){"use strict";n.d(t,"b",function(){return o}),n.d(t,"c",function(){return i}),n.d(t,"a",function(){return r}),n.d(t,"k",function(){return a}),n.d(t,"l",function(){return s}),n.d(t,"j",function(){return l}),n.d(t,"h",function(){return c}),n.d(t,"i",function(){return u}),n.d(t,"g",function(){return d}),n.d(t,"e",function(){return p}),n.d(t,"f",function(){return v}),n.d(t,"d",function(){return f});var o="CHANGE_INVITATION_STATUS_START",i="CHANGE_INVITATION_STATUS_SUCCESS",r="CHANGE_INVITATION_STATUS_FAILED",a="GET_NOTIFIES_START",s="GET_NOTIFIES_SUCCESS",l="GET_NOTIFIES_FAILED",c="GET_INVITES_START",u="GET_INVITES_SUCCESS",d="GET_INVITES_FAILED",p="DOWNLOAD_COURSE_FILE_START",v="DOWNLOAD_COURSE_FILE_SUCCESS",f="DOWNLOAD_COURSE_FILE_FAILED"},"./app/containers/NotificationPage/index.js":function(e,t,n){"use strict";function o(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:C,t=arguments[1];switch(t.type){case T.b:return"Accepted"===t.status?e.set("pending",!0).set("needUpdate",!1):e.set("pending",!0).set("needUpdate",!0);case T.c:return t.groupId&&"Accepted"===t.status&&location.assign("/group/"+t.groupId),e.set("pending",!1).set("needUpdate",!1);case T.a:return e.set("pending",!1).set("error",!0).set("needUpdate",!1);case T.k:return e.set("pending",!0);case T.l:return e.set("pending",!1).set("notifies",t.notifies);case T.j:return e.set("pending",!1).set("error",!0);case T.h:return e.set("pending",!0);case T.i:return e.set("pending",!1).set("invites",t.invites);case T.g:return e.set("pending",!1).set("error",!0);case T.e:return e.set("pending",!0);case T.f:var n=document.createElement("a"),o=URL.createObjectURL(t.file);return n.href=o,n.download="plan",document.body.appendChild(n),n.click(),setTimeout(function(){document.body.removeChild(n),URL.revokeObjectURL(o)},0),e.set("pending",!1).set("error",!1);case T.d:return _.message.error("Не удалось загузить файл!"),e.set("pending",!1).set("error",!0);default:return e}}function i(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function a(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function s(e){return{downloadCourseFile:function(t){return e(Object(N.d)(t))}}}function l(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function c(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function u(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function d(e){return{acceptInvitation:function(t,n){return e(Object(N.a)(t,n))},declineInvitation:function(t,n){return e(Object(N.a)(t,n))}}}function p(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function v(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function f(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function h(e){return{getNotifies:function(){return e(Object(N.j)())},getInvites:function(){return e(Object(N.g)())}}}Object.defineProperty(t,"__esModule",{value:!0});var y=n("./node_modules/react/react.js"),b=n.n(y),g=(n("./node_modules/prop-types/index.js"),n("./node_modules/react-redux/es/index.js")),m=n("./node_modules/reselect/es/index.js"),S=n("./node_modules/redux/es/index.js"),O=n("./app/utils/injectSaga.js"),w=n("./app/utils/injectReducer.js"),j=function(e){return e.get("notificationPage")},x=n("./node_modules/immutable/dist/immutable.js"),T=n("./app/containers/NotificationPage/constants.js"),_=n("./node_modules/antd/lib/index.js"),C=Object(x.fromJS)({notifies:[],invites:[],pending:!1,error:!1,needUpdate:!1}),D=o,E=n("./app/containers/NotificationPage/saga.js"),N=n("./app/containers/NotificationPage/actions.js"),I=n("./app/globalJS.js"),k=(n("./app/config.js"),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,i){var r=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&r)for(var s in r)void 0===n[s]&&(n[s]=r[s]);else n||(n=r||{});if(1===a)n.children=i;else if(a>1){for(var l=Array(a),c=0;c<a;c++)l[c]=arguments[c+3];n.children=l}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}()),R=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),A=k("div",{className:"not-readed-btn"}),U=function(e){function t(e){return i(this,t),r(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return a(t,e),R(t,[{key:"render",value:function(){var e=this,t=JSON.parse(this.props.notificationInfo);return k(_.Card,{hoverable:!0,className:"notify-card",style:{width:"100%",cursor:"default",position:"relative",overflow:"visible"},bodyStyle:{padding:"14px 20px"}},void 0,A,k(_.Row,{},void 0,k(_.Col,{span:16},void 0,k("span",{},void 0,1===this.props.notificationType?"Преподаватель "+t.TeacherName+' завершил курс в группе "'+t.GroupTitle+'"':2===this.props.notificationType?'Учебный план группы "'+t.GroupTitle+'" принят':3===this.props.notificationType?"Пользователь "+t.DeclinedName+' отклонил учебный план группы "'+t.GroupTitle+'"':4===this.props.notificationType?k("div",{},void 0,k("span",{style:{display:"block"}},void 0,"В группе ",t.GroupTitle," предложен учебный план"),k("a",{target:"_blank",onClick:function(){return e.props.downloadCourseFile(t.CurriculumLink)}},void 0,"Скачать учебный план")):5===this.props.notificationType?'Группа "'+t.GroupTitle+'" сформирована':6===this.props.notificationType?"Пользователь "+t.InvitedName+' принял ваше приглашение в группу "'+t.GroupTitle+'"':7===this.props.notificationType?"Пользователь "+t.InvitedName+' отклонил ваше приглашение в группу "'+t.GroupTitle+'"':8===this.props.notificationType?"Пользователь "+t.InviterName+' пригласил вас в группу "'+t.GroupTitle+'" на роль "'+Object(I.e)(t.SuggestedRole)+'"':9===this.props.notificationType?"Пользователь "+t.Username+' покинул группу "'+t.GroupTitle+'"':10===this.props.notificationType?"Пользователь "+t.NewCreatorUsername+' стал новым создателем группы "'+t.GroupTitle+'"':11===this.props.notificationType?"Пользователь "+t.Username+' присоединился в группу "'+t.GroupTitle+'"':12===this.props.notificationType?"Пользователь "+t.SenderName+" отправил репорт на пользователя "+t.SuspectedName+' за нарушение правила "'+t.BrokenRule+'"':13===this.props.notificationType?"Пользователь "+t.ReviewerName+' оставил отзыв о преподавателе группы "'+t.GroupTitle+'"':14===this.props.notificationType?'К вам была применена санкция "'+Object(I.g)(t.SanctionType)+'" за нарушение правила "'+t.BrokenRule+'"':15===this.props.notificationType?"К пользователю "+t.Username+' была применена санкция "'+Object(I.g)(t.SanctionType)+'" за нарушение правила "'+t.BrokenRule+'"':16===this.props.notificationType?"Пользователь "+t.TeacherName+' стал новым учителем группы "'+t.GroupTitle+'"':17===this.props.notificationType?'Примененная к вам санкция "'+Object(I.g)(t.SanctionType)+'" за нарушение правила "'+t.BrokenRule+'" была отменена':18===this.props.notificationType?"Примененная к пользователю "+t.Username+' санкция "'+Object(I.g)(t.SanctionType)+'" за нарушение правила "'+t.BrokenRule+'" была отменена':"")),k(_.Col,{span:8,style:{textAlign:"right"}},void 0,k("span",{style:{fontSize:14,opacity:.7}},void 0,(new Date(this.props.occurredOn).getDate()<10?"0"+new Date(this.props.occurredOn).getDate():new Date(this.props.occurredOn).getDate())+"."+(new Date(this.props.occurredOn).getMonth()<10?"0"+new Date(this.props.occurredOn).getMonth():new Date(this.props.occurredOn).getMonth())+"."+new Date(this.props.occurredOn).getFullYear()+"\n                      "+(new Date(this.props.occurredOn).getHours()<10?"0"+new Date(this.props.occurredOn).getHours():new Date(this.props.occurredOn).getHours())+":"+(new Date(this.props.occurredOn).getMinutes()<10?"0"+new Date(this.props.occurredOn).getMinutes():new Date(this.props.occurredOn).getMinutes())))),k(_.Row,{},void 0,k("span",{style:{wordWrap:"break-word"}},void 0,this.props.text)))}}]),t}(b.a.PureComponent),P=Object(m.b)({}),B=Object(g.b)(P,s)(U),G=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,i){var r=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&r)for(var s in r)void 0===n[s]&&(n[s]=r[s]);else n||(n=r||{});if(1===a)n.children=i;else if(a>1){for(var l=Array(a),c=0;c<a;c++)l[c]=arguments[c+3];n.children=l}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),M=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),L=G("div",{className:"not-readed-btn"}),F=function(e){function t(e){l(this,t);var n=c(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.tryAccept=n.tryAccept.bind(n),n.tryDecline=n.tryDecline.bind(n),n}return u(t,e),M(t,[{key:"tryAccept",value:function(){"true"!==localStorage.getItem("without_server")&&this.props.acceptInvitation(this.props.id,"Accepted")}},{key:"tryDecline",value:function(){"true"!==localStorage.getItem("without_server")&&this.props.declineInvitation(this.props.id,"Declined")}},{key:"render",value:function(){return G(_.Card,{hoverable:!0,className:"notify-card",style:{width:"100%",cursor:"default"},bodyStyle:{padding:"14px 20px 0 20px"}},void 0,L,G(_.Row,{style:{marginBottom:12}},void 0,G(_.Col,{span:12},void 0,G("span",{style:{fontSize:14,opacity:.9}},void 0,this.props.fromUserName))),G(_.Row,{},void 0,G(_.Col,{xs:{span:24},sm:{span:12},style:{marginBottom:10}},void 0,G("span",{style:{wordWrap:"break-word"}},void 0,'Вас пригласили в группу "',this.props.toGroupTitle,'" на роль "',Object(I.e)(this.props.suggestedRole),'"')),G(_.Col,{xs:{span:24},sm:{span:12},style:{textAlign:"right"}},void 0,G(_.Button,{type:"primary",style:{marginRight:12,marginBottom:14},onClick:this.tryAccept},void 0,"Принять"),G(_.Button,{onClick:this.tryDecline},void 0,"Отклонить"))))}}]),t}(b.a.PureComponent),J=Object(m.b)({}),z=Object(g.b)(J,d),H=Object(w.a)({key:"notificationPage",reducer:D}),V=Object(O.a)({key:"notificationPage",saga:E.a}),$=Object(S.c)(H,V,z)(F);n.d(t,"NotificationPage",function(){return Dt});var W=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var o in n)Object.prototype.hasOwnProperty.call(n,o)&&(e[o]=n[o])}return e},q=function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,o,i){var r=t&&t.defaultProps,a=arguments.length-3;if(n||0===a||(n={}),n&&r)for(var s in r)void 0===n[s]&&(n[s]=r[s]);else n||(n=r||{});if(1===a)n.children=i;else if(a>1){for(var l=Array(a),c=0;c<a;c++)l[c]=arguments[c+3];n.children=l}return{$$typeof:e,type:t,key:void 0===o?null:""+o,ref:null,props:n,_owner:null}}}(),K=function(){function e(e,t){for(var n=0;n<t.length;n++){var o=t[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}return function(t,n,o){return n&&e(t.prototype,n),o&&e(t,o),t}}(),Y=_.Tabs.TabPane,Q=_.Form.Item,X=_.Select.Option,Z=[{id:"1233123124",fromUser:"Имя отправителя",date:(new Date).toDateString(),text:"Текст уведомления",readed:!1},{id:"dasqdsad",fromUser:"Имя отправителя",date:(new Date).toDateString(),text:"Текст уведомления",readed:!0},{id:"qwersifhxjk",fromUser:"Имя отправителя",date:(new Date).toDateString(),text:"Текст уведомления",readed:!0}],ee=[{id:"dasqdsadsdfsdf",fromUser:"fdspijfjlkf",fromUserName:"Имя отправителя",date:(new Date).toDateString(),toGroup:"32478643981654",toGroupTitle:"JS Juniors",suggestedRole:"Участник",readed:!1},{id:"dasqdsddsfwee",fromUser:"324gj3h4j1",fromUserName:"Имя отправителя",date:(new Date).toDateString(),toGroup:"dr32847363274",toGroupTitle:"C# Juniors",suggestedRole:"Участник",readed:!0}],te=q(_.Row,{},void 0,q(_.Divider,{})),ne=q(X,{value:"Default"},void 0,"По умолчанию"),oe=q(X,{value:"DontSend"},void 0,"Не отправлять"),ie=q(X,{value:"ToMail"},void 0,"На почту"),re=q(X,{value:"OnSite"},void 0,"На сайте"),ae=q(X,{value:"Everywhere"},void 0,"Везде"),se=q(X,{value:"Default"},void 0,"По умолчанию"),le=q(X,{value:"DontSend"},void 0,"Не отправлять"),ce=q(X,{value:"ToMail"},void 0,"На почту"),ue=q(X,{value:"OnSite"},void 0,"На сайте"),de=q(X,{value:"Everywhere"},void 0,"Везде"),pe=q(X,{value:"Default"},void 0,"По умолчанию"),ve=q(X,{value:"DontSend"},void 0,"Не отправлять"),fe=q(X,{value:"ToMail"},void 0,"На почту"),he=q(X,{value:"OnSite"},void 0,"На сайте"),ye=q(X,{value:"Everywhere"},void 0,"Везде"),be=q(X,{value:"Default"},void 0,"По умолчанию"),ge=q(X,{value:"DontSend"},void 0,"Не отправлять"),me=q(X,{value:"ToMail"},void 0,"На почту"),Se=q(X,{value:"OnSite"},void 0,"На сайте"),Oe=q(X,{value:"Everywhere"},void 0,"Везде"),we=q(X,{value:"Default"},void 0,"По умолчанию"),je=q(X,{value:"DontSend"},void 0,"Не отправлять"),xe=q(X,{value:"ToMail"},void 0,"На почту"),Te=q(X,{value:"OnSite"},void 0,"На сайте"),_e=q(X,{value:"Everywhere"},void 0,"Везде"),Ce=q(X,{value:"Default"},void 0,"По умолчанию"),De=q(X,{value:"DontSend"},void 0,"Не отправлять"),Ee=q(X,{value:"ToMail"},void 0,"На почту"),Ne=q(X,{value:"OnSite"},void 0,"На сайте"),Ie=q(X,{value:"Everywhere"},void 0,"Везде"),ke=q(X,{value:"Default"},void 0,"По умолчанию"),Re=q(X,{value:"DontSend"},void 0,"Не отправлять"),Ae=q(X,{value:"ToMail"},void 0,"На почту"),Ue=q(X,{value:"OnSite"},void 0,"На сайте"),Pe=q(X,{value:"Everywhere"},void 0,"Везде"),Be=q(X,{value:"Default"},void 0,"По умолчанию"),Ge=q(X,{value:"DontSend"},void 0,"Не отправлять"),Me=q(X,{value:"ToMail"},void 0,"На почту"),Le=q(X,{value:"OnSite"},void 0,"На сайте"),Fe=q(X,{value:"Everywhere"},void 0,"Везде"),Je=q(X,{value:"Default"},void 0,"По умолчанию"),ze=q(X,{value:"DontSend"},void 0,"Не отправлять"),He=q(X,{value:"ToMail"},void 0,"На почту"),Ve=q(X,{value:"OnSite"},void 0,"На сайте"),$e=q(X,{value:"Everywhere"},void 0,"Везде"),We=q(X,{value:"Default"},void 0,"По умолчанию"),qe=q(X,{value:"DontSend"},void 0,"Не отправлять"),Ke=q(X,{value:"ToMail"},void 0,"На почту"),Ye=q(X,{value:"OnSite"},void 0,"На сайте"),Qe=q(X,{value:"Everywhere"},void 0,"Везде"),Xe=q(X,{value:"Default"},void 0,"По умолчанию"),Ze=q(X,{value:"DontSend"},void 0,"Не отправлять"),et=q(X,{value:"ToMail"},void 0,"На почту"),tt=q(X,{value:"OnSite"},void 0,"На сайте"),nt=q(X,{value:"Everywhere"},void 0,"Везде"),ot=q(X,{value:"Default"},void 0,"По умолчанию"),it=q(X,{value:"DontSend"},void 0,"Не отправлять"),rt=q(X,{value:"ToMail"},void 0,"На почту"),at=q(X,{value:"OnSite"},void 0,"На сайте"),st=q(X,{value:"Everywhere"},void 0,"Везде"),lt=q(X,{value:"Default"},void 0,"По умолчанию"),ct=q(X,{value:"DontSend"},void 0,"Не отправлять"),ut=q(X,{value:"ToMail"},void 0,"На почту"),dt=q(X,{value:"OnSite"},void 0,"На сайте"),pt=q(X,{value:"Everywhere"},void 0,"Везде"),vt=q(X,{value:"Default"},void 0,"По умолчанию"),ft=q(X,{value:"DontSend"},void 0,"Не отправлять"),ht=q(X,{value:"ToMail"},void 0,"На почту"),yt=q(X,{value:"OnSite"},void 0,"На сайте"),bt=q(X,{value:"Everywhere"},void 0,"Везде"),gt=q(X,{value:"Default"},void 0,"По умолчанию"),mt=q(X,{value:"DontSend"},void 0,"Не отправлять"),St=q(X,{value:"ToMail"},void 0,"На почту"),Ot=q(X,{value:"OnSite"},void 0,"На сайте"),wt=q(X,{value:"Everywhere"},void 0,"Везде"),jt=q(X,{value:"Default"},void 0,"По умолчанию"),xt=q(X,{value:"DontSend"},void 0,"Не отправлять"),Tt=q(X,{value:"ToMail"},void 0,"На почту"),_t=q(X,{value:"OnSite"},void 0,"На сайте"),Ct=q(X,{value:"Everywhere"},void 0,"Везде"),Dt=function(e){function t(e){p(this,t);var n=v(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.showSettingBtn=function(){document.getElementById("setting-btn").classList.remove("notify-setting-btn-hidden"),document.getElementById("setting-btn").classList.add("notify-setting-btn")},n.hideSettingBtn=function(){document.getElementById("setting-btn").classList.remove("notify-setting-btn"),document.getElementById("setting-btn").classList.add("notify-setting-btn-hidden")},n.state={needUpdate:n.props.needUpdate,isEditing:!1},n.showSettingBtn=n.showSettingBtn.bind(n),n.hideSettingBtn=n.hideSettingBtn.bind(n),n}return f(t,e),K(t,[{key:"componentDidMount",value:function(){"true"===localStorage.getItem("without_server")?this.setState({notifies:this.notifies,invites:this.invites}):(this.props.getNotifies(),this.props.getInvites())}},{key:"componentDidUpdate",value:function(e,t){e.needUpdate!==this.props.needUpdate&&(this.props.getNotifies(),this.props.getInvites())}},{key:"render",value:function(){var e=this;return q(_.Row,{className:"notify-tabs"},void 0,this.state.isEditing?q(_.Col,{xs:{span:22,offset:1},sm:{span:20,offset:2},lg:{span:12,offset:6}},void 0,q(_.Row,{style:{textAlign:"center",marginTop:40}},void 0,q("h4",{style:{marginBottom:0}},void 0,"Настройка уведомлений")),te,q(_.Row,{style:{marginTop:0}},void 0,q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,ne,oe,ie,re,ae))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,se,le,ce,ue,de))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,pe,ve,fe,he,ye))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,be,ge,me,Se,Oe))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,we,je,xe,Te,_e))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,Ce,De,Ee,Ne,Ie))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,ke,Re,Ae,Ue,Pe))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,Be,Ge,Me,Le,Fe))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,Je,ze,He,Ve,$e))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,We,qe,Ke,Ye,Qe))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,Xe,Ze,et,tt,nt))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,ot,it,rt,at,st))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,lt,ct,ut,dt,pt))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,vt,ft,ht,yt,bt))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,gt,mt,St,Ot,wt))),q(Q,{labelCol:{xs:{span:24},sm:{span:14}},colon:!1,label:"Уведомления о завершении курса"},void 0,q(_.Col,{className:"xs-text-align-left"},void 0,q(_.Select,{style:{width:180}},void 0,jt,xt,Tt,_t,Ct))),q(_.Col,{style:{marginTop:20,textAlign:"center",marginBottom:40}},void 0,q(_.Button,{htmlType:"button",onClick:function(){return e.setState({isEditing:!1})},style:{marginRight:"4%"}},void 0,"Отменить"),q(_.Button,{type:"primary",onClick:function(){return e.setState({isEditing:!1})}},void 0,"Подтвердить")))):q(_.Col,{className:"notify-area",onMouseEnter:this.showSettingBtn,onMouseLeave:this.hideSettingBtn,xs:{span:22,offset:1},sm:{span:20,offset:2},lg:{span:12,offset:6}},void 0,q(_.Icon,{id:"setting-btn",onClick:function(){return e.setState({isEditing:!0})},className:"notify-setting-btn-hidden",type:"setting"}),q(_.Tabs,{defaultActiveKey:"1",style:{margin:"30px 0"}},void 0,q(Y,{tab:"Уведомления",style:{margin:"30px 0"}},"1","true"===localStorage.getItem("withoutServer")?q("div",{},void 0,Z.reverse().map(function(e){return b.a.createElement(B,W({key:e.id},e))})):q("div",{},void 0,this.props.notifies.map(function(e,t){return b.a.createElement(B,W({key:t},e))}))),q(Y,{tab:"Приглашения",style:{margin:"30px 0"}},"2","true"===localStorage.getItem("withoutServer")?q("div",{},void 0,ee.reverse().map(function(e){return b.a.createElement($,W({key:e.id},e))})):q("div",{},void 0,this.props.invites.map(function(e){return b.a.createElement($,W({key:e.id},e))}))))))}}]),t}(b.a.Component),Et=Object(m.b)({notifies:function(){return Object(m.a)(j,function(e){return e.get("notifies")})}(),invites:function(){return Object(m.a)(j,function(e){return e.get("invites")})}(),needUpdate:function(){return Object(m.a)(j,function(e){return e.get("needUpdate")})}()}),Nt=Object(g.b)(Et,h),It=Object(w.a)({key:"notificationPage",reducer:D}),kt=Object(O.a)({key:"notificationPage",saga:E.a});t.default=Object(S.c)(It,kt,Nt)(Dt)},"./app/containers/NotificationPage/saga.js":function(e,t,n){"use strict";(function(e){function o(e){var t,n;return regeneratorRuntime.wrap(function(o){for(;;)switch(o.prev=o.next){case 0:return o.prev=0,t=e.status,o.next=4,Object(p.a)(l,e.invitationId,e.status);case 4:return n=o.sent,o.next=7,Object(p.b)(Object(h.c)(n.groupId,t));case 7:o.next=13;break;case 9:return o.prev=9,o.t0=o.catch(0),o.next=13,Object(p.b)(Object(h.b)(o.t0));case 13:case"end":return o.stop()}},y,this,[[0,9]])}function i(){var e;return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(p.a)(a);case 3:return e=t.sent,t.next=6,Object(p.b)(Object(h.l)(e.reverse()));case 6:t.next=12;break;case 8:return t.prev=8,t.t0=t.catch(0),t.next=12,Object(p.b)(Object(h.k)(t.t0));case 12:case"end":return t.stop()}},b,this,[[0,8]])}function r(){var e;return regeneratorRuntime.wrap(function(t){for(;;)switch(t.prev=t.next){case 0:return t.prev=0,t.next=3,Object(p.a)(s);case 3:return e=t.sent,t.next=6,Object(p.b)(Object(h.i)(e.invitations.reverse()));case 6:t.next=12;break;case 8:return t.prev=8,t.t0=t.catch(0),t.next=12,Object(p.b)(Object(h.h)(t.t0));case 12:case"end":return t.stop()}},g,this,[[0,8]])}function a(){return e(v.a.API_BASE_URL+"/user/profile/notifications",{headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).then(function(e){return e.json()}).catch(function(e){return e})}function s(){return e(v.a.API_BASE_URL+"/user/profile/invitations",{headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")}}).then(function(e){return e.json()}).then(function(e){return e}).catch(function(e){return e})}function l(t,n){return e(v.a.API_BASE_URL+"/user/profile/invitations",{method:"PUT",headers:{"Content-Type":"application/json-patch+json",Authorization:"Bearer "+localStorage.getItem("token")},body:JSON.stringify({invitationId:t,status:n})}).then(function(e){return e.json()}).catch(function(e){return e})}function c(e){var t;return regeneratorRuntime.wrap(function(n){for(;;)switch(n.prev=n.next){case 0:return n.prev=0,n.next=3,Object(p.a)(u,e.link);case 3:return t=n.sent,n.next=6,Object(p.b)(Object(h.f)(t));case 6:n.next=12;break;case 8:return n.prev=8,n.t0=n.catch(0),n.next=12,Object(p.b)(Object(h.e)(n.t0));case 12:case"end":return n.stop()}},m,this,[[0,8]])}function u(t){return e(v.a.API_BASE_URL+"/file/"+t,{method:"GET",headers:{Authorization:"Bearer "+localStorage.getItem("token")}}).then(function(e){return e.blob()}).then(function(e){return e}).catch(function(e){return e})}function d(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.next=2,Object(p.c)(f.b,o);case 2:return e.next=4,Object(p.c)(f.k,i);case 4:return e.next=6,Object(p.c)(f.h,r);case 6:return e.next=8,Object(p.c)(f.e,c);case 8:case"end":return e.stop()}},S,this)}t.a=d;var p=n("./node_modules/redux-saga/es/effects.js"),v=n("./app/config.js"),f=n("./app/containers/NotificationPage/constants.js"),h=n("./app/containers/NotificationPage/actions.js"),y=regeneratorRuntime.mark(o),b=regeneratorRuntime.mark(i),g=regeneratorRuntime.mark(r),m=regeneratorRuntime.mark(c),S=regeneratorRuntime.mark(d)}).call(t,n("./node_modules/exports-loader/index.js?self.fetch!./node_modules/whatwg-fetch/fetch.js"))}});