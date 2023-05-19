namespace BogoLib;

public static class Math
{
    /// <summary>
    /// Returns the absolute value of a specified number.
    /// </summary>
    /// <param name="x">Number whose absolute value you want to know</param>
    /// <returns>Returns the positive of x</returns>
    public static float Abs(float x)
        => x < 0 ? -x : x;

    /// <summary>
    /// Returns the absolute value of a specified number.
    /// </summary>
    /// <param name="x">Number whose absolute value you want to know</param>
    /// <param name="atol">Absolute tolerance for the convergence of the algorithm</param>
    /// <param name="maxIter">Maximum number of iterations of the algorithm</param>
    /// <param name="verbose">Write information about numerical errors to the console</param>
    /// <returns>Returns the positive of x</returns>
    public static double Abs(double x)
        => x < 0 ? -x : x;

    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="x">Number whose root you want to know</param>
    /// <param name="atol">Absolute tolerance for the convergence of the algorithm</param>
    /// <param name="maxIter">Maximum number of iterations of the algorithm</param>
    /// <param name="verbose">Write information about numerical errors to the console</param>
    /// <returns>Returns the positive square root of x</returns>
    public static float Sqrt(float x, float atol = 1E-2F, int maxIter = 10000, bool verbose = false)
    {
        if (float.IsNaN(x) || x < 0)
            return float.NaN;

        if (float.IsPositiveInfinity(x))
            return float.PositiveInfinity;

        // Initial guess
        float sqrt = x * System.Random.Shared.Next(0, 100000) / 100000;  // Random value
        // float sqrt = x / System.MathF.Pow(10, (int)(System.MathF.Log10(x) / 2));  // Good initial approximation

        float exp = sqrt * sqrt;
        float minSqrt = 0;
        float maxSqrt = x;

        float factor = System.MathF.Pow(10, (int)System.MathF.Log10(maxSqrt));
        float aux = sqrt;

        float rand;
        int minRand;
        int maxRand;

        int iter = 0;

        do
        {
            do
            {
                if (exp < x)
                    minSqrt = aux > minSqrt ? aux : minSqrt;
                else if (aux > 0)
                    maxSqrt = aux < maxSqrt ? aux : maxSqrt;

                minRand = -9; // (Int32)(((aux % (10 * factor)) - (aux % factor)) / factor) * MAGIC;
                maxRand = 10; // (1 + (Int32)(((max_f % (10 * factor)) - (max_f % factor)) / factor)) * MAGIC;

                rand = sqrt + factor * System.Random.Shared.Next(minRand, maxRand);
                aux = rand > 0 ? rand : 0;
                exp = aux * aux;

                if (Abs(exp - x) < Abs(sqrt * sqrt - x))
                    sqrt = aux;

                iter++;

            } while ((maxSqrt - minSqrt > 1.9F * factor) && (maxSqrt - minSqrt >= atol) && (iter < maxIter));

            factor /= 10;

        } while ((maxSqrt - minSqrt >= atol) && (iter < maxIter));

        exp = sqrt * sqrt;

        if (verbose)
        {
            float sqrtBetter = sqrt;

            for (byte _ = 0; _ < 8; _++)
                sqrtBetter = sqrt - (sqrt * sqrt - x) / (sqrt + sqrtBetter);  // Custom algorithm, provides better accuracy
                // sqrtBetter = (sqrtBetter + x / sqrtBetter) / 2;  // Newton's method

            System.Console.WriteLine($"Iterations: {iter}");
            System.Console.WriteLine($"Max error: {Abs(maxSqrt - minSqrt):E15}");
            System.Console.WriteLine($"Squared error: {Abs(exp - x):E15}");
            System.Console.WriteLine($"Aprox. error: {Abs(sqrt - sqrtBetter):E15}");
        }

        return sqrt;
    }

    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="x">Number whose root you want to know</param>
    /// <param name="atol">Absolute tolerance for the convergence of the algorithm</param>
    /// <param name="maxIter">Maximum number of iterations of the algorithm</param>
    /// <param name="verbose">Write information about numerical errors to the console</param>
    /// <returns>Returns the positive square root of x</returns>
    public static double Sqrt(double x, double atol = 1E-6, int maxIter = 10000, bool verbose = false)
    {
        if (double.IsNaN(x) || x < 0)
            return double.NaN;

        if (double.IsPositiveInfinity(x))
            return double.PositiveInfinity;

        // Initial guess
        double sqrt = x * System.Random.Shared.Next(0, 100000) / 100000;  // Random value
        // double sqrt = x / System.MathF.Pow(10, (int)(System.MathF.Log10(x) / 2));  // Good initial approximation

        double exp = sqrt * sqrt;
        double minSqrt = 0;
        double maxSqrt = x;

        double factor = System.Math.Pow(10, (int)System.Math.Log10(maxSqrt));
        double aux = sqrt;

        double rand;
        int minRand;
        int maxRand;

        int iter = 0;

        do
        {
            do
            {
                if (exp < x && aux >= 0)
                    minSqrt = aux > minSqrt ? aux : minSqrt;
                else if (exp > x && aux >= 0)
                    maxSqrt = aux < maxSqrt ? aux : maxSqrt;

                minRand = -9; // (Int32)(((aux % (10 * factor)) - (aux % factor)) / factor) * MAGIC;
                maxRand = 10; // (1 + (Int32)(((max_f % (10 * factor)) - (max_f % factor)) / factor)) * MAGIC;

                rand = sqrt + factor * System.Random.Shared.Next(minRand, maxRand);
                aux = rand > 0 ? rand : 0;
                exp = aux * aux;

                if (Abs(exp - x) < Abs(sqrt * sqrt - x))
                    sqrt = aux;

                iter++;

            } while ((maxSqrt - minSqrt > 1.9 * factor) && (maxSqrt - minSqrt >= atol) && (iter < maxIter));

            factor /= 10;

        } while ((maxSqrt - minSqrt >= atol) && (iter < maxIter));

        exp = sqrt * sqrt;

        if (verbose)
        {
            double sqrtBetter = sqrt;

            for (byte _ = 0; _ < 8; _++)
                sqrtBetter = sqrt - (sqrt * sqrt - x) / (sqrt + sqrtBetter);  // Custom algorithm, provides better accuracy
                // sqrtBetter = (sqrtBetter + x / sqrtBetter) / 2;  // Newton's method

            System.Console.WriteLine($"Iterations: {iter}");
            System.Console.WriteLine($"Max error: {Abs(maxSqrt - minSqrt):E15}");
            System.Console.WriteLine($"Squared error: {Abs(exp - x):E15}");
            System.Console.WriteLine($"Aprox. error: {Abs(sqrt - sqrtBetter):E15}");
        }

        return sqrt;
    }

    /// <summary>
    /// Returns the root of a specified function.
    /// </summary>
    /// <param name="func">Function whose root you want to know</param>
    /// <param name="x0">Smallest value for the analysis interval, optional</param>
    /// <param name="x1">Hghest value for the analysis interval, optional</param>
    /// <param name="atol">Absolute tolerance for the convergence of the algorithm</param>
    /// <param name="maxIter">Maximum number of iterations of the algorithm</param>
    /// <param name="verbose">Write information about numerical errors to the console</param>
    /// <returns>Returns the root of func</returns>
    public static float Root(System.Func<float, float> func, float x0 = float.NaN, float x1 = float.NaN, float atol = 1E-6F, int maxIter = 1000, bool verbose = false)
    {
        float left = x0;
        float right = x1;
        float rt = float.NaN;
        float y = float.NaN;

        int iter = 0;

        if (float.IsNaN(left))
            left = - 0.1F * float.MaxValue * System.Random.Shared.NextSingle();

        if (float.IsNaN(right))
            right = 0.1F * float.MaxValue * System.Random.Shared.NextSingle();

        for (iter = 0; iter < maxIter; iter++)
        {
            rt = left + (right - left) * System.Random.Shared.NextSingle();
            y = func(rt);

            if (y * func(left) < 0)
                right = rt;
            else
                left = rt;

            iter++;

            if (Abs(right - left) <= atol)
                break;
        }

        if (verbose)
        {
            float rtBetter = rt;

            for (byte _ = 0; _ < 8; _++)
                rtBetter -= func(rtBetter) * 1E-6F / (func(rtBetter) - func(rtBetter - 1E-6F));  // Newton's method

            System.Console.WriteLine($"Iterations: {iter}");
            System.Console.WriteLine($"Aprox. root: {rt:E15}");
            System.Console.WriteLine($"Max error: {Abs(right - left):E15}");
            System.Console.WriteLine($"Aprox. error: {Abs(rt - rtBetter):E15}");
        }

        if (Abs(right - left) <= atol)
            return rt;

        throw new System.Exception("Convergence not successul");
    }

    /// <summary>
    /// Returns the root of a specified function.
    /// </summary>
    /// <param name="func">Function whose root you want to know</param>
    /// <param name="x0">Smallest value for the analysis interval, optional</param>
    /// <param name="x1">Hghest value for the analysis interval, optional</param>
    /// <param name="atol">Absolute tolerance for the convergence of the algorithm</param>
    /// <param name="maxIter">Maximum number of iterations of the algorithm</param>
    /// <param name="verbose">Write information about numerical errors to the console</param>
    /// <returns>Returns the root of func</returns>
    public static double Root(System.Func<double, double> func, double x0 = double.NaN, double x1 = double.NaN, double atol = 1E-16, int maxIter = 10000, bool verbose = false)
    {
        double left = x0;
        double right = x1;
        double rt = double.NaN;
        double y = double.NaN;

        int iter = 0;

        if (double.IsNaN(left))
            left = - 0.1 * double.MaxValue * System.Random.Shared.NextDouble();

        if (double.IsNaN(right))
            right = 0.1 * double.MaxValue * System.Random.Shared.NextDouble();

        for (iter = 0; iter < maxIter; iter++)
        {
            rt = left + (right - left) * System.Random.Shared.NextDouble();
            y = func(rt);

            if (y * func(left) < 0)
                right = rt;
            else
                left = rt;

            iter++;

            if (Abs(right - left) <= atol)
                break;
        }

        if (verbose)
        {
            double rtBetter = rt;

            for (byte _ = 0; _ < 8; _++)
                rtBetter -= func(rtBetter) * 1E-12 / (func(rtBetter) - func(rtBetter - 1E-12));  // Newton's method

            System.Console.WriteLine($"Iterations: {iter}");
            System.Console.WriteLine($"Aprox. root: {rt:E15}");
            System.Console.WriteLine($"Max error: {Abs(right - left):E15}");
            System.Console.WriteLine($"Aprox. error: {Abs(rt - rtBetter):E15}");
        }

        if (Abs(right - left) <= atol)
            return rt;

        throw new System.Exception("Convergence not successul");
    }
}
