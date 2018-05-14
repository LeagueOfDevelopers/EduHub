import { createSelector } from 'reselect';

/**
 * Direct selector to the sendResetPasswordInfoPage state domain
 */
const selectSendResetPasswordInfoPageDomain = (state) => state.get('sendResetPasswordInfoPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by ResetPasswordAcceptedPage
 */

const makeSelectSendResetPasswordInfoPage = () => createSelector(
  selectSendResetPasswordInfoPageDomain,
  (substate) => substate.toJS()
);

export default makeSelectSendResetPasswordInfoPage;
export {
  selectSendResetPasswordInfoPageDomain,
};
