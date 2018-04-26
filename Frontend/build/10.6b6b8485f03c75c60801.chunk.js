webpackJsonp([10],{"./app/components/InviteMemberSelect/index.js":function(e,t,n){"use strict";function r(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function i(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function o(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}function s(e){return{searchInvitationUsers:function(t,n){return e(Object(d._2)(t,n))},inviteMember:function(t,n,r){return e(Object(d.W)(t,n,r))}}}Object.defineProperty(t,"__esModule",{value:!0});var a=n("./node_modules/react/react.js"),l=n.n(a),u=n("./node_modules/prop-types/index.js"),c=(n.n(u),n("./node_modules/react-redux/es/index.js")),p=n("./node_modules/reselect/es/index.js"),d=n("./app/containers/GroupPage/actions.js"),b=n("./app/containers/GroupPage/selectors.js"),f=n("./node_modules/antd/lib/index.js"),h=(n.n(f),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,n,r,i){var o=t&&t.defaultProps,s=arguments.length-3;if(n||0===s||(n={}),n&&o)for(var a in o)void 0===n[a]&&(n[a]=o[a]);else n||(n=o||{});if(1===s)n.children=i;else if(s>1){for(var l=Array(s),u=0;u<s;u++)l[u]=arguments[u+3];n.children=l}return{$$typeof:e,type:t,key:void 0===r?null:""+r,ref:null,props:n,_owner:null}}}()),v=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),m=function(e){function t(e){r(this,t);var n=i(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.handleVisibleChange=function(e){n.setState({inviteVisible:e}),setTimeout(function(){return n.setState({inviteMemberRole:""})},300)},n.handleSelectChange=function(e){n.setState({selectValue:e}),setTimeout(function(){return n.props.searchInvitationUsers(n.props.groupId,e)},0)},n.state={inviteVisible:!1,selectValue:"",inviteMemberRole:null},n.tryInviteMember=n.tryInviteMember.bind(n),n.handleVisibleChange=n.handleVisibleChange.bind(n),n.handleSelectChange=n.handleSelectChange.bind(n),n}return o(t,e),v(t,[{key:"tryInviteMember",value:function(e){var t=this;"true"!==localStorage.getItem("without_server")?"Teacher"===this.state.inviteMemberRole?this.props.inviteMember(this.props.groupId,e,"Teacher"):"Member"===this.state.inviteMemberRole&&this.props.inviteMember(this.props.groupId,e,"Member"):f.message.success("Приглашение отправлено"),setTimeout(function(){return t.setState({selectValue:""})},0)}},{key:"render",value:function(){var e=this;return h(f.Dropdown,{overlay:h(f.Menu,{},void 0,this.state.inviteMemberRole?h(f.Menu.Item,{className:"unhover"},void 0,h(f.Select,{mode:"combobox",className:"unhover",style:{width:"100%"},value:this.state.selectValue,onChange:this.handleSelectChange,placeholder:"Введите имя пользователя",defaultActiveFirstOption:!1,showArrow:!1},void 0,this.props.users.map(function(t){return h(f.Select.Option,{},t.username,h("div",{onClick:function(){return e.tryInviteMember(t.id)}},void 0,t.username))}))):h(f.Menu.Item,{className:"unhover"},"0",h(f.Button,{onClick:function(){return e.setState({inviteMemberRole:"Member"})},style:{display:"block",width:"100%",marginBottom:1}},void 0,"Участник"),h(f.Button,{onClick:function(){return e.setState({inviteMemberRole:"Teacher"})},style:{display:"block",width:"100%"}},void 0,"Учитель"))),onVisibleChange:this.handleVisibleChange,visible:this.state.inviteVisible,trigger:["click"]},void 0,h(f.Button,{className:"md-offset-16px",style:{width:"100%"},type:"primary"},void 0,"Пригласить"))}}]),t}(l.a.Component);m.defaultProps={users:"true"===localStorage.getItem("withoutServer")?["Первый пользователь","Второй пользователь"]:[]};var y=Object(p.b)({users:Object(b.d)()}),g=Object(c.b)(y,s);t.default=g(m)}});