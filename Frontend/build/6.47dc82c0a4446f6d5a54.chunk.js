webpackJsonp([6],{"./app/components/MembersList/index.js":function(e,t,o){"use strict";function n(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function r(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}function i(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}Object.defineProperty(t,"__esModule",{value:!0});var a=o("./node_modules/react/react.js"),s=o.n(a),l=o("./node_modules/antd/lib/index.js"),c=(o.n(l),o("./node_modules/react-router-dom/index.js")),p=(o.n(c),o("./node_modules/prop-types/index.js")),u=(o.n(p),function(){var e="function"==typeof Symbol&&Symbol.for&&Symbol.for("react.element")||60103;return function(t,o,n,r){var i=t&&t.defaultProps,a=arguments.length-3;if(o||0===a||(o={}),o&&i)for(var s in i)void 0===o[s]&&(o[s]=i[s]);else o||(o=i||{});if(1===a)o.children=r;else if(a>1){for(var l=Array(a),c=0;c<a;c++)l[c]=arguments[c+3];o.children=l}return{$$typeof:e,type:t,key:void 0===n?null:""+n,ref:null,props:o,_owner:null}}}()),f=function(){function e(e,t){for(var o=0;o<t.length;o++){var n=t[o];n.enumerable=n.enumerable||!1,n.configurable=!0,"value"in n&&(n.writable=!0),Object.defineProperty(e,n.key,n)}}return function(t,o,n){return o&&e(t.prototype,o),n&&e(t,n),t}}(),d=u(l.Col,{},void 0,"Участников"),m=function(e){function t(e){n(this,t);var o=r(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return o.confirm=o.confirm.bind(o),o}return i(t,e),f(t,[{key:"confirm",value:function(){l.message.error("Участник удален")}},{key:"render",value:function(){var e=this;return u("div",{style:{width:280,boxShadow:"rgba(0, 0, 0, 0.4) 0px 0px 6px -2px"}},void 0,u(l.Row,{type:"flex",justify:"space-between",style:{padding:"6px 16px",boxShadow:"0px 2px 6px -2px rgba(0,0,0,0.36)"}},void 0,d,u(l.Col,{},void 0,this.props.members.length+"/"+this.props.size)),u("div",{className:"member-container"},void 0,u(l.List,{dataSource:this.props.members,renderItem:function(t){return u(l.List.Item,{},t.id,u(l.List.Item.Meta,{avatar:u(l.Avatar,{src:t.avatarLink}),title:u(c.Link,{to:"#"},void 0,t.name),description:t.member.memberRole}),"Создатель"!==t.role?u(l.Popconfirm,{title:"Удалить участника?",onConfirm:e.confirm,okText:"Да",cancelText:"Нет"},void 0,u(l.Icon,{style:{fontSize:18,cursor:"pointer"},type:"close"})):"")}},void 0)))}}]),t}(s.a.Component);m.defaultProps={name:"",count:0,size:0,role:"",members:[],id:""},t.default=m}});