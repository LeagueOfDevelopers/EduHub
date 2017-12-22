import React, { Component } from 'react';
import PropTypes from 'prop-types';

class LotDescription extends React.Component {
    constructor(props) {
        super(props);
    }
    static defaultProps = {
        description: 'undefined'
    }
    static PropTypes = {
        description: PropTypes.string
    }
    render() {
        return (
            <div style={{ marginLeft: 20, fontSize: 14, overflowY: 'auto' }}>
                <p>
                    {this.props.description}
                </p>
            </div>
        );
    }
}

export default LotDescription;
