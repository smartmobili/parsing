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
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using CommonErrorMessages = com.habelitz.core.resource.resbundle.CommonErrorMessages;
using JSOM = com.habelitz.jsobjectizer.jsom.JSOM;
using JSOMType = com.habelitz.jsobjectizer.jsom.JSOMType;
using com.habelitz.jsobjectizer.jsom.api.statement;
using TraverseAction = com.habelitz.jsobjectizer.jsom.util.TraverseAction;
using AST2JSOMTree = com.habelitz.jsobjectizer.unmarshaller.antlrbridge.AST2JSOMTree;
using com.habelitz.jsobjectizer.unmarshaller.jsombridge.ast2api;

namespace com.habelitz.jsobjectizer.unmarshaller.jsombridge.ast2api.statement {
/**
 * This <code>StatementBlockElement</code> type represents a local class
 * declaration.
 * <p>
 * Note that instances of this class are of the JSOM type <code>
 * JSOMType.STATEMENT_BLOCK_ELEMENT</code> instead of <code>
 * JSOMType.CLASS_DECLARATION</code>.
 * 
 * @author Dieter Habelitz
 */
public class AST2LocalClassDeclaration 
    : AST2ClassDeclaration 
    , LocalClassDeclaration, AST2StatementBlockElement {

    /**
     * Constructor.
     * 
     * @param pTree  The tree node representing local class declaration.
     * @param pTokenRewriteStream  The token stream the token of the stated
     *                             tree node belongs to.            
     */
    public AST2LocalClassDeclaration(AST2JSOMTree pTree, TokenRewriteStream pTokenRewriteStream) 
        : base(pTree, JSOMType.STATEMENT_BLOCK_ELEMENT, pTokenRewriteStream)
    {
        
        
    }

    // Because this class : 'AST2ClassDeclaration' it can not also
    // extend 'AST2StatementBlockElement'. So the methods
    // 'getStatementBlockElement()' and
    // 'isStatementBlockElementType()' must get overridden here.

    /**
     * Returns the <code>ElementType</code> represented by <code>this</code>.
     * 
     * @return  The <code>ElementType</code> represented by <code>this</code>.
     */
    public ElementType getStatementBlockElementType() {
        
        return ElementType.LOCAL_CLASS_DECLARATION;
    }

    /**
     * Always returns <code>false</code>.
     * 
     * @return  <code>false</code>
     */
    public bool isStatement() {
        
        return false;
    }

    /**
     * Returns <code>true</code> if <code>pType</code> equals to <code>
     * StatementBlockElement.ElementType.LOCAL_CLASS_DECLARATION</code>.
     * 
     * @return  <code>true</code> if <code>pType</code> equals to <code>
     *          StatementBlockElement.ElementType.LOCAL_CLASS_DECLARATION</code>.
     */
    public override bool isStatementBlockElementType(ElementType pType) {

        return pType == ElementType.LOCAL_CLASS_DECLARATION;
    }

    /**
     * Calls the methods <code>pAction.performAction(...)</code> and <code>
     * pAction.actionPerformed(...)</code> with <code>this</code> as argument.
     * <P>
     * Furthermore, if <code>pAction.isMemberTraversionEnabled() == true</code>
     * all <code>JSOM</code> members that belong to <code>this
     * </code> will be traversed by calling the <code>traverseAll(...)</code> 
     * method on these members with <code>pAction</code> as argument. This will
     * be done after the <code>pAction.performAction(...)</code> call but before
     * the <code>pAction.actionPerformed(...)</code> call.
     * 
     * @see  JSOM#traverseAll(TraverseAction)
     * 
     * @param pAction  User define traverse actions. 
     * 
     * @throws  NullPointerException if <code>pAction</code> is <code>
     *          null</code>.
     */ 
    public override void traverseAll(TraverseAction pAction) {
        
        pAction.performAction(this);
        if (pAction.isMemberTraversingEnabled()) {
            base.traverseAll(pAction, true);
        }
        pAction.actionPerformed(this);
    }
}
}