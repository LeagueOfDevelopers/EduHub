import React from 'react';
import {Component} from "react/cjs/react.production.min";
import {connect} from 'react-redux';
import PrimaryBtn from '../../components/default-btn';
import {Link} from "react-router-dom";
import {getUser} from "./actions";

class Header extends Component{
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        this.props.getUser();
    }

    render() {
       return (
           <div className="header-item" uk-sticky="bottom: #offset">
               <Link to='/' className="uk-button uk-button-text">Home</Link>
               <div className="account-settings">
                   <Link to='/account' className="uk-button uk-button-text">Sign in</Link>
                   <PrimaryBtn title="Sign up" />
               </div>
           </div>
       );
    }



    };

    function mapStateToProps(state) {
        return {
            user: state.userState
        }
    }

    function mapDispatchToProps(dispatch) {
        return {
            getUser:() => dispatch(getUser())
        }
    }

    export default connect(mapStateToProps, mapDispatchToProps)(Header)