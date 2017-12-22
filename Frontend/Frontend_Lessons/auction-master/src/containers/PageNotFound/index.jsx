import React, { Component } from 'react';
import pageNotFoundImage from '../../Pictures/pageNotFound.png';

const PageNotFound = () => {
    return(
        <div style={{ height: '100vh', width: '100%', overflow: 'hidden' }}>
            <img src={pageNotFoundImage}
                style={{ width: '100%', height: '100%' }}
            >
            </img>
        </div>
    )
}

export default PageNotFound;