/*
 * BSD license
 * 
 * Copyright (c) 2007-2011 by HABELITZ Software Developments
 *
 * All rights reserved.
 * 
 * http://www.habelitz.com
 *
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 *  3. The name of the author may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY HABELITZ SOFTWARE DEVELOPMENTS ('HSD') ``AS IS'' 
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
 * ARE DISCLAIMED. IN NO EVENT SHALL 'HSD' BE LIABLE FOR ANY DIRECT, INDIRECT, 
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT 
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;

namespace com.habelitz.jsobjectizer.jsom.api.statement
{

    /**
     * This <code>Statement</code> type represents a labeled statement.
     * <p>
     * Formal example:
     *  <pre>
     *      // the label
     *      labelId: 
     *      // the statement
     *      while (anyCondition) { 
     *          while (anyCondition) {
     *              if (anyCondition) {
     *                  break labelId:
     *              }
     *          }
     *      }
     *  </pre>
     * 
     * @author Dieter Habelitz
     */
    public interface LabeledStatement : Statement
    {

        /**
         * Returns the label's identifier.
         * 
         * @return  The label's identifier.
         */
        /*public*/ String getIdentifier();

        /**
         * Returns the labeled statement.
         * 
         * @return  The labeled statement.
         */
        /*public*/ Statement getStatement();

        /**
         * Replaces the identifier of <code>this</code>.
         * 
         * @param pNewIdentifier  The new identifier of the label.
         * 
         * @return  The old identifier.
         */
        /*public*/ String setIdentifier(String pNewIdentifier);
    }
}