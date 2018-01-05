import { createSelector } from 'reselect';

/**
 * Direct selector to the registrationPage state domain
 */
const selectRegistrationPage = (state) => state.get('registrationPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by RegistrationPage
 */

export {
  selectRegistrationPage,
};
