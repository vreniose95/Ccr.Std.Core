using System;
using System.Runtime.CompilerServices;
using Ccr.Std.Core.Numerics.Infrastructure;
using Ccr.Std.Core.Numerics.Ranges;
using JetBrains.Annotations;
using static Ccr.Std.Core.Numerics.Infrastructure.EndpointExclusivity;
using static JetBrains.Annotations.AssertionConditionType;

// ReSharper disable BuiltInTypeReferenceStyle
namespace Ccr.Std.Core.Extensions.NumericExtensions
{
  public static class ByteExtensions
  {
    /// <summary>
    ///		Extension method that uses the less-than operator to return either the extension method
    ///   subject <paramref name="this"/>, or the parameter <see cref="value"/>, whichever value is
    ///   determined to be lesser.
    /// </summary>
    /// <param name="this">
    ///		The extension method subject <see cref="Byte"/> in which to perform the comparison upon.
    /// </param>
    /// <param name="value">
    ///		A value of type <see cref="Byte"/> in which to perform the comparison against the extension 
    ///		method's subject, the <paramref name="this"/> parameter.
    /// </param>
    /// <returns>
    ///		Returns the lesser numeric value of the extention method's <see cref="Byte"/> subject, and
    ///   the <paramref name="value"/> parameter. 
    /// </returns>
    public static Byte Smallest(
      this Byte @this,
      Byte value)
    {
      return @this < value
        ? value
        : @this;
    }

    /// <summary>
    ///		Extension method that uses the greater-than operator to return either the extension method
    ///   subject <paramref name="this"/>, or the parameter <see cref="value"/>, whichever value is
    ///   determined to be greater.
    /// </summary>
    /// <param name="this">
    ///		The extension method subject <see cref="Byte"/> in which to perform the comparison upon.
    /// </param>
    /// <param name="value">
    ///		A value of type <see cref="Byte"/> in which to perform the comparison against the extension 
    ///		method's subject, the <paramref name="this"/> parameter.
    /// </param>
    /// <returns>
    ///		Returns the greater numeric value of the extention method's <see cref="Byte"/> subject,
    ///   and the <paramref name="value"/> parameter. 
    /// </returns>
    public static Byte Largest(
      this Byte @this,
      Byte value)
    {
      return @this > value
        ? value
        : @this;
    }

    /// <summary>
    ///		Extension method that performs a transformation on the <see cref="Byte"/> subject using
    ///		linear mapping to re-map from the provided initial range <paramref name="startRange"/> 
    ///		to the target range <paramref name="endRange"/>.
    /// </summary>
    /// <param name="this">
    ///		The subject <see cref="Byte"/> in which to perform the linear map range re-mapping upon.
    /// </param>
    /// <param name="startRange">
    ///		An instance of the type <see cref="ByteRange"/>, describing a range of a pair of numeric
    ///   <see cref="Byte"/> values in which the linear re-mapping uses as the inital range.
    /// </param>
    /// <param name="endRange">
    ///		An instance of the type <see cref="ByteRange"/>, describing a range of a pair of numeric
    ///   <see cref="Byte"/> values in which the linear re-mapping uses as the target range.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///		Thrown when either the <paramref name="startRange"/> or the <paramref name="endRange"/> 
    ///		parameters are equal to <see langword="null"/>.
    ///	</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///		Thrown when either the <paramref name="@this"/> subject parameter is not within the bounds
    ///   of the provided <paramref name="startRange"/>. This is determined with a call to the
    ///   <see cref="IsNotWithin"/> method, with an implicit <see cref="EndpointExclusivity"/> value
    ///   of <see cref="EndpointExclusivity.Inclusive"/> by default, unless it is specified explicitly
    ///   set this property to <see cref="EndpointExclusivity.Exclusive"/>.
    ///	</exception>
    /// <returns>
    ///		A <see cref="Byte"/> value that has been linearly mapped on the <paramref name="startRange"/>
    ///		parameter, and re-mapped to the <paramref name="endRange"/> parameter.
    /// </returns>
    public static Byte LinearMap(
      this Byte @this,
      [NotNull] ByteRange startRange,
      [NotNull] ByteRange endRange)
    {
      startRange.IsNotNull(nameof(startRange));
      endRange.IsNotNull(nameof(endRange));

      if (@this.IsNotWithin(startRange))
        throw new ArgumentOutOfRangeException(
          nameof(@this),
          @this,
          $"The {nameof(@this).SQuote()} parameter value is outside of the acceptable range. The " +
          $"value must be within the {nameof(startRange).SQuote()} parameter. The current value is " +
          $"{@this.ToString().SQuote()}, and the {nameof(startRange).SQuote()} parameter range is " +
          $"{startRange}.");

      var linearMapped
        = (@this - startRange.Minimum) *
          (endRange.Maximum - endRange.Minimum) /
          (startRange.Maximum - startRange.Minimum) +
          endRange.Minimum;

      return Convert
        .ToByte(
          linearMapped);
    }

    /// <summary>
    ///		Extension method that checks if the extension method subject <see cref="@this"/> is
    ///   within the specified <see cref="ByteRange"/>.
    /// </summary>
    /// <param name="this">
    ///		The subject <see cref="Byte"/> value in which to check against the <paramref name="range"/>
    ///		parameter to determine whether it is within the range, taking into account the exclusivity.
    /// </param>
    /// <param name="range">
    ///		An instance of the type <see cref="ByteRange"/>, describing a range of a pair of numeric
    ///   <see cref="Byte"/> values in which the <paramref name="this"/> subject is to be compared
    ///   against.
    /// </param>
    /// <param name="exclusivity">
    ///		A value indicating whether to perform the upper and lower bounds comparisons including
    ///		the range's Minimum and Maximum bounds, or to exclude them. This parameter is optional,
    ///		and the default value is <see cref="EndpointExclusivity.Inclusive"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///		Thrown when the <paramref name="range"/> parameter are equal to <see langword="null"/>.
    ///	</exception>
    /// <returns>
    ///		A <see cref="bool"/> value indicating whether or not the <paramref name="this"/> subject
    ///		is within the provided <paramref cref="range"/> parameter, taking into account the 
    ///		<see cref="EndpointExclusivity"/> mode via the <paramref name="exclusivity"/> parameter.
    /// </returns>
    public static bool IsWithin(
      this Byte @this,
      [NotNull] ByteRange range,
      EndpointExclusivity exclusivity = Inclusive)
    {
      range.IsNotNull(nameof(range));

      return range
        .IsWithin(
          @this,
          exclusivity);
    }

    /// <summary>
    ///		Extension method that allows for <see cref="IntegralRangeBase{TIntegralType}.IsNotWithin"/> 
    ///		to be called on a <see cref="Byte"/> subject with the range and exclusivity passed as a
    ///		parameter, rather than on the <see cref="IntegralRangeBase{TIntegralType}"/> object 
    ///		with a <see cref="Byte"/> parameter.
    /// </summary>
    /// <param name="this">
    ///		The subject <see cref="Byte"/> value in which to check against the <paramref name="range"/>
    ///		parameter to determine whether it is within the range, taking into account the exclusivity.
    /// </param>
    /// <param name="range">
    ///		An instance of the type <see cref="ByteRange"/>, describing a range of numeric values in 
    ///		which the <paramref name="this"/> subject is to be compared against.
    /// </param>
    /// <param name="exclusivity">
    ///		A value indicating whether to perform the upper and lower bounds comparisons including
    ///		the range's Minimum and Maximum bounds, or to exclude them. This parameter is optional,
    ///		and the default value is <see cref="EndpointExclusivity.Inclusive"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///		Thrown when the specified <paramref name="range"/> is <see langword="null"/>.
    ///	</exception>
    /// <returns>
    ///		A <see cref="Byte"/> value indicating whether or not the <paramref name="this"/> subject
    ///		is within the provided <paramref cref="range"/> parameter, taking into account the 
    ///		<see cref="EndpointExclusivity"/> mode via the <paramref name="exclusivity"/> parameter.
    ///		This comparison is the logical inverse of the <see cref="IsNotWithin"/> extension method.
    /// </returns>
    /// <remarks>
    ///   The behavior of this method can change based on the <paramref name="exclusivity"/> parameter
    ///   value. The <paramref name="exclusivity"/> parameter is optional, and has the default value
    ///   of <see cref="EndpointExclusivity.Inclusive"/>. When executing any range comparison
    ///   statement, there are two different methodologies of Range value interpretation. The value
    ///   comparison assumes by default that the desired execution functionality is
    ///   <see cref="EndpointExclusivity.Inclusive"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    ///		Thrown when either the <paramref name="range"/> or the <paramref name="endRange"/> 
    ///		parameters are equal to <see langword="null"/>.
    ///	</exception>
    /// <example>
    ///   <list type="table">
    ///     <listHeader>
    ///       <termn>Exclusivitiy</termn>
    ///       <termn>Expression</termn>
    ///     </listHeader>  
    ///     <term/>
    ///     <term><see cref="EndpointExclusivity.Exclusive"/></term>
    ///     <term> [ range.Low &lt; @this &lt; range.High ] </term>
    ///     <term/> 
    ///     <term/>
    ///     <term><see cref="EndpointExclusivity.Inclusive"/></term>
    ///     <term> [ range.Low &ge; = @this &ge; = range.High ] </term>
    ///     <term/> 
    ///   </list>
    /// </example>
    public static bool IsNotWithin(
      this Byte @this,
      [NotNull] ByteRange range,
      EndpointExclusivity exclusivity = Inclusive)
    {
      range.IsNotNull(nameof(range));

      return range
        .IsNotWithin(
          @this,
          exclusivity);
    }


    /// <summary>
    ///   Assertion method that ensures that the provided <paramref name="this"/> numerical value
    ///   parameter subject does not lie within the bounds provided by the <paramref name="range"/>
    ///   range parameter. If <paramref name="this"/> is within the bounds provided by the
    ///   <paramref name="range"/> range parameter, the method will halt execution, and throw a
    ///   <see cref="ArgumentOutOfRangeException"/>. 
    /// </summary>
    /// <param name="this">
    ///   The <see cref="Byte"/> value used as the subject parameter of the assertion method. This is
    ///   the value in which to check against the <paramref name="range"/> parameter, to ensure that
    ///   <paramref name="this"/> does not lie within the bounds provided by the <paramref name="range"/>
    ///   range parameter.
    /// </param>
    /// <param name="range">
    ///		An instance of the type <see cref="ByteRange"/>, describing a range of numeric values in 
    ///		which the <paramref name="this"/> subject is to be compared against.
    /// </param>
    /// <param name="elementName">
    ///   This element is a "optional" string that you do not have the option to use. The value is
    ///   automatically provided and assigned at compile/runtime through the Microsoft's Compiiler
    ///   Service Attribute, and in this particular case it we are using primarily the directive
    ///   <see cref="CallerMemberNameAttribute"/>, as well as the compiler service attribute
    ///   <see cref="InvokerParameterNameAttribute"/> is used on the <paramref name="elementName"/>
    ///   which simply ensures that you that the Intellisense list will be correctly optimized.
    /// </param>
    /// <param name="exclusivity">
    ///		A value indicating whether to perform the upper and lower bounds comparisons including
    ///		the range's Minimum and Maximum bounds, or to exclude them. This parameter is optional,
    ///		and the default value is <see cref="EndpointExclusivity.Inclusive"/>.
    /// </param>
    /// <param name="callerMemberName">
    ///   The <see cref="string"/> name indicating the calling context's name.
    /// </param>
    [ContractAnnotation("this:null => halt"), AssertionMethod]
    public static void ThrowIfWithin(
      [AssertionCondition(IS_NOT_NULL)] this Byte @this,
      [NotNull] ByteRange range,
      [InvokerParameterName] string elementName,
      EndpointExclusivity exclusivity = Inclusive,
      [CallerMemberName] string callerMemberName = "")
    {
      range.IsNotNull(nameof(range));

      if (range
        .IsWithin(
          @this,
          exclusivity))
        throw new ArgumentOutOfRangeException(
          elementName,
          $"Parameter {elementName.SQuote()} passed to the method {callerMemberName.SQuote()} " +
          $"cannot be within [{range.Minimum} and {range.Maximum}], {exclusivity}ly.");
    }

    [ContractAnnotation("this:null => halt"), AssertionMethod]
    public static void ThrowIfNotWithin(
      [AssertionCondition(IS_NOT_NULL)] this Byte @this,
      [NotNull] ByteRange range,
      [InvokerParameterName] string elementName,
      EndpointExclusivity exclusivity = Inclusive,
      [CallerMemberName] string callerMemberName = "")
    {
      range.IsNotNull(nameof(range));

      if (range
        .IsNotWithin(
          @this,
          exclusivity))
        throw new ArgumentOutOfRangeException(
          elementName,
          $"Parameter {elementName.SQuote()} passed to the method {callerMemberName.SQuote()} " +
          $"must be within [{range.Minimum} and {range.Maximum}], {exclusivity}ly.");
    }

    /// <summary>
    ///		Extension method that allows for <see cref="IntegralRangeBase{TIntegralType}.Constrain"/> 
    ///		to be called on a <see cref="Byte"/> subject with the range and exclusivity passed as 
    ///		parameter(s), rather than on the <see cref="IntegralRangeBase{TIntegralType}"/> object 
    ///		with a <see cref="Byte"/> parameter.
    /// </summary>
    /// <param name="this">
    ///		The subject <see cref="Byte"/> value in which to check against the <paramref name="range"/>
    ///		parameter to constrain a value within a range with an implicit inclusive comparison mode.
    /// </param>
    /// <param name="range">
    ///		An instance of the type <see cref="ByteRange"/>, describing a range of numeric values in 
    ///		which the <paramref name="this"/> subject is to be compared against.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///		Thrown when the specified <paramref name="range"/> is <see langword="null"/>.
    ///	</exception>
    /// <returns>
    ///		A <see cref="Byte"/> value that is the <paramref name="this"/> subject value adjusted to
    ///		force the range of possible values to be within the provided <paramref cref="range"/> 
    ///		parameter, using <see cref="EndpointExclusivity.Inclusive"/> as the comparison mode.
    /// </returns>
    public static Byte Constrain(
      this Byte @this,
      [NotNull] ByteRange range)
    {
      range.IsNotNull(nameof(range));

      return range
        .Constrain(
          @this);
    }

    /// <summary>
    ///   
    /// </summary>
    /// <param name="this"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    public static byte ScaleDown(
      this byte @this,
      double percent)
    {
      if (percent.IsNotWithin((0d, 100d)))
        throw new ArgumentOutOfRangeException(
          nameof(percent),
          $"The {nameof(percent).SQuote()} parameter must be within 0 and 100, inclusively.");

      var value = @this - @this * (percent / 1d);

      var constrained = value
        .Constrain(
          (byte.MinValue, byte.MaxValue));

      return Convert.ToByte(constrained);
    }

    public static byte ScaleUp(
      this byte @this,
      double percent)
    {
      var value = @this + @this * (percent / 1);

      if (value > byte.MaxValue)
        value = byte.MaxValue;

      if (value < byte.MinValue)
        value = byte.MinValue;

      return Convert.ToByte(value);
    }


  }
}